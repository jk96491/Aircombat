import tensorflow as tf

class Actor:
    def __init__(self, name, state_size, action_size):
        with tf.variable_scope(name):
            self.state = tf.placeholder(tf.float32, [None, state_size])
            '''
            참고한 링크
            https://www.tensorflow.org/api_docs/python/tf/keras/layers/Conv2D
            https://www.tensorflow.org/api_docs/python/tf/keras/layers/MaxPool2D?hl=ko
            https://www.tensorflow.org/tutorials/images/cnn?hl=ko
            https://blog.naver.com/PostView.nhn?blogId=laonple&logNo=221377293443
            '''
            self.conv1 = tf.keras.layers.conv2D(self.state,32, (3,3),activation='relu')
            self.pooling1 = tf.keras.layers.MaxPool2D(self.conv1,(2,2))
            self.conv2 = tf.keras.layers.conv2D(self.pooling1,32,(3,3),activation='relu')
            self.pooling2 = tf.keras.layers.MaxPool2D(self.conv2,(2, 2))
            self.conv3 = tf.keras.layers.conv2D(self.pooling2,64, (3, 3), activation='relu')

            self.fc1 = tf.layers.dense(self.conv3, 128, activation=tf.nn.relu, kernel_regularizer=tf.contrib.layers.l2_regularizer(scale=0.8))
            self.fc2 = tf.layers.dense(self.fc1, 128, activation=tf.nn.relu, kernel_regularizer=tf.contrib.layers.l2_regularizer(scale=0.8))
            self.action = tf.layers.dense(self.fc2, action_size, activation=tf.nn.tanh) # 액션 값이 -1 ~ 1사이로 나오게 하려고 tanh 사용

        self.trainable_var = tf.get_collection(tf.GraphKeys.TRAINABLE_VARIABLES, name)


