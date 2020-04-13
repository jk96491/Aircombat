import tensorflow as tf

class Critic:
    def __init__(self, name, state_size, action_size):
        with tf.variable_scope(name):
            self.state = tf.placeholder(tf.float32, [None, state_size])

            self.conv1 = tf.keras.layers.conv2D(self.state, 32, (3, 3), activation='relu')
            self.pooling1 = tf.keras.layers.MaxPool2D(self.conv1, (2, 2))
            self.conv2 = tf.keras.layers.conv2D(self.pooling1, 32, (3, 3), activation='relu')
            self.pooling2 = tf.keras.layers.MaxPool2D(self.conv2, (2, 2))
            self.conv3 = tf.keras.layers.conv2D(self.pooling2, 64, (3, 3), activation='relu')

            self.fc1 = tf.layers.dense(self.conv3, 128, activation=tf.nn.relu,
                                       kernel_regularizer=tf.contrib.layers.l2_regularizer(scale=0.8))
            self.action = tf.placeholder(tf.float32, [None, action_size])
            self.concat = tf.concat([self.fc1, self.action], axis=-1)
            self.fc2 = tf.layers.dense(self.concat, 128, activation=tf.nn.relu,
                                       kernel_regularizer=tf.contrib.layers.l2_regularizer(scale=0.8))
            self.fc3 = tf.layers.dense(self.fc2, 128, activation=tf.nn.relu,
                                       kernel_regularizer=tf.contrib.layers.l2_regularizer(scale=0.8))
            self.predict_q = tf.layers.dense(self.fc3, 1, activation=None)

        self.trainable_var = tf.get_collection(tf.GraphKeys.TRAINABLE_VARIABLES, name)
