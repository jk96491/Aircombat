import numpy as np

mu = 0.6
theta = 1e-5
sigma = 1e-2

class OU_noise:
    def __init__(self, action_size, scale_):
        self.action_size = action_size
        self.reset()
        self.scale = scale_

    def reset(self):
        self.X = np.ones(self.action_size) * mu

    def sample(self):
        dx = theta * (mu - self.X) + sigma * np.random.randn(len(self.X))
        self.X += dx
        return self.X

    def StandardNoise(self, action_):
        noisy_action = np.random.normal(loc=0.0, scale=self.scale, size=action_.shape)
        return np.clip(noisy_action, -1.0, 1.0)



