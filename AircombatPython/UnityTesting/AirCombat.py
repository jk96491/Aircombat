import tensorflow as tf
import numpy as np
import random
import datetime
from collections import deque
from mlagents.envs.environment import UnityEnvironment

state_size = 8
action_size = 3

load_model = False
train_mode = True

batch_size =128
mem_maxlen = 50000
discount_factor = 0.99
actor_lr = 1e-4
critic_lr = 5e-4
tau = 1e-3

mu = 0.6
theta = 1e-5
sigma = 1e-2

start_train_episode = 10
run_episode = 500000
test_episode = 100000

print_interval = 5
save_interval = 100

date_time = datetime.datetime.now().strftime("%Y%m%d-%H-%M-%S")

game = "AirCombat"
env_name = "AirCombat.exe"


save_path = 'SaveModels/' + date_time + "_DDPG"
load_path = 'SaveModels/20200229-15-50-44_DDPG/model/model'


# OU_noise 클래스 -> ou noise 정의 및 파라미터 결정
class OU_noise:
    def __init__(self):
        self.reset()

    def reset(self):
        self.X = np.ones(action_size) * mu

    def sample(self):
        dx = theta * (mu - self.X) + sigma * np.random.randn(len(self.X))
        self.X += dx
        return self.X


# Actor 클래스 -> Actor 클래스를 통해 행동을 출력
class Actor:
    def __init__(self, name):
        with tf.variable_scope(name):
            self.state = tf.placeholder(tf.float32, [None, state_size])
            self.fc1 = tf.layers.dense(self.state, 128, activation=tf.nn.relu)
            self.fc2 = tf.layers.dense(self.fc1, 128, activation=tf.nn.relu)
            self.action = tf.layers.dense(self.fc2, action_size, activation=tf.nn.tanh) # 액션 값이 -1 ~ 1사이로 나오게 하려고 tanh 사용

        self.trainable_var = tf.get_collection(tf.GraphKeys.TRAINABLE_VARIABLES, name)


class Critic:
    def __init__(self, name):
        with tf.variable_scope(name):
            self.state = tf.placeholder(tf.float32, [None, state_size])
            self.fc1 = tf.layers.dense(self.state, 128, activation=tf.nn.relu)
            self.action = tf.placeholder(tf.float32, [None, action_size])
            self.concat = tf.concat([self.fc1, self.action], axis=-1)
            self.fc2 = tf.layers.dense(self.concat, 128, activation=tf.nn.relu)
            self.fc3 = tf.layers.dense(self.fc2, 128, activation=tf.nn.relu)
            self.predict_q = tf.layers.dense(self.fc3, 1, activation=None)

        self.trainable_var = tf.get_collection(tf.GraphKeys.TRAINABLE_VARIABLES, name)

class DDPGAgent:
    def __init__(self):
        self.actor = Actor('actor')
        self.critic = Critic('critic')
        self.target_actor = Actor('target_actor')
        self.target_critic = Critic('target_critic')

        # Critic Model 학습용 (Q 함수)
        self.target_q = tf.placeholder(tf.float32, [None, 1])
        critic_loss = tf.losses.mean_squared_error(self.target_q, self.critic.predict_q)
        self.train_critic = tf.train.AdamOptimizer(critic_lr).minimize(critic_loss)

        # Actor Model 학습용
        action_grad = tf.gradients(tf.squeeze(self.critic.predict_q), self.critic.action)
        policy_grad = tf.gradients(self.actor.action, self.actor.trainable_var, action_grad)
        for idx, grads in enumerate(policy_grad):
            policy_grad[idx] -= grads / batch_size
        self.train_actor = tf.train.AdamOptimizer(actor_lr).apply_gradients(zip(policy_grad, self.actor.trainable_var))

        self.sess = tf.Session()
        self.sess.run(tf.global_variables_initializer())

        self.Saver = tf.train.Saver()
        self.Summary, self.Merge = self.Make_Summary()
        self.OU = OU_noise()
        self.memory = deque(maxlen=mem_maxlen)

        self.soft_update_target = []
        for idx in range(len(self.actor.trainable_var)):
            softTau = (tau * self.actor.trainable_var[idx].value() + (1 - tau) * self.target_actor.trainable_var[idx].value())
            self.target_actor.trainable_var[idx].assign(softTau)
            self.soft_update_target.append(self.target_actor.trainable_var[idx])
        for idx in range(len(self.critic.trainable_var)):
            softTau = (tau * self.critic.trainable_var[idx].value()) + ((1 - tau) * self.target_critic.trainable_var[idx].value())
            self.target_critic.trainable_var[idx].assign(softTau)
            self.soft_update_target.append(self.target_critic.trainable_var[idx].assign(softTau))

        init_update_target = []
        for idx in range(len(self.actor.trainable_var)):
            init_update_target.append(self.target_actor.trainable_var[idx].assign(self.actor.trainable_var[idx]))
        for idx in range(len(self.critic.trainable_var)):
            init_update_target.append(self.target_critic.trainable_var[idx].assign(self.critic.trainable_var[idx]))

        self.sess.run(init_update_target)

        if load_model:
            self.Saver.restore(self.sess, load_path)

    def get_action(self, state):
        action = self.sess.run(self.actor.action, feed_dict={self.actor.state: state})
        noise = self.OU.sample()

        if train_mode:
            return action + noise
        else:
            return action


    def append_sample(self, state, action, reward, next_state, done):
        self.memory.append((state, action, reward, next_state, done))

    def save_model(self):
        self.Saver.save(self.sess, save_path + "/model/model")

    def train_model(self):
        mini_batch = random.sample(self.memory, batch_size)
        states = np.asarray([sample[0] for sample in mini_batch])

        actions = np.asarray([sample[1] for sample in mini_batch])
        rewards = np.asarray([sample[2] for sample in mini_batch])
        next_states = np.asarray([sample[3] for sample in mini_batch])
        dones = np.asarray([sample[4] for sample in mini_batch])

        target_actor_actions = self.sess.run(self.target_actor.action, feed_dict={self.target_actor.state: next_states})
        target_critic_predict_qs = self.sess.run(self.target_critic.predict_q, feed_dict={self.target_critic.state: next_states
            , self.target_critic.action: target_actor_actions})

        target_qs = np.asarray([reward + discount_factor * (1 - done) * target_critic_predict_q
                                for reward, target_critic_predict_q, done in zip(
                rewards, target_critic_predict_qs, dones)])

        self.sess.run(self.train_critic, feed_dict={self.critic.state: states,
                                                    self.critic.action: actions,
                                                    self.target_q: target_qs})

        actions_for_train = self.sess.run(self.actor.action, feed_dict={self.actor.state: states})
        self.sess.run(self.train_actor, feed_dict={self.actor.state: states,
                                                   self.critic.state: states,
                                                   self.critic.action: actions_for_train})

        self.sess.run(self.soft_update_target)

    def Make_Summary(self):
        self.summary_reward = tf.placeholder(tf.float32)
        self.summary_success_cnt = tf.placeholder(tf.float32)
        tf.summary.scalar("reward", self.summary_reward)
        tf.summary.scalar("success_cnt", self.summary_success_cnt)
        Summary = tf.summary.FileWriter(logdir=save_path, graph=self.sess.graph)
        Merge = tf.summary.merge_all()

        return Summary, Merge

    def Write_Summray(self, reward, success_cnt, episode):
        self.Summary.add_summary(self.sess.run(self.Merge, feed_dict={
            self.summary_reward: reward,
            self.summary_success_cnt: success_cnt}), episode)


if __name__ == '__main__':
    env = UnityEnvironment(file_name=env_name)
    default_brain = env.brain_names[0]

    agent = DDPGAgent()
    rewards = deque(maxlen=print_interval)
    success_cnt = 0
    step = 0

    for episode in range(run_episode + test_episode):
        if episode == run_episode:
            train_mode = False

        env_info = env.reset(train_mode=train_mode)[default_brain]
        state = env_info.vector_observations[0]
        episode_rewards = 0
        done = False

        while not done:
            step += 1

            action = agent.get_action([state])[0]
            #print(action)
            env_info = env.step(action)[default_brain]
            next_state = env_info.vector_observations[0]
            reward = env_info.rewards[0]
            done = env_info.local_done[0]

            episode_rewards += reward

            if train_mode:
                agent.append_sample(state, action, reward, next_state, done)

            state = next_state

            if episode > start_train_episode and train_mode:
                agent.train_model()

        success_cnt = success_cnt + 1 if reward == 1 else success_cnt
        rewards.append(episode_rewards)

        if episode % print_interval == 0 and episode != 0:
            print("step: {} / episode: {} / reward: {:.3f} / success_cnt: {}".format
                  (step, episode, np.mean(rewards), success_cnt))
            agent.Write_Summray(np.mean(rewards), success_cnt, episode)
            success_cnt = 0

        if train_mode and episode % save_interval == 0 and episode != 0:
            print("model saved")
            agent.save_model()

    env.close()






