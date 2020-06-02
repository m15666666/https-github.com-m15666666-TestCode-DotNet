"""
import numpy as np
import matplotlib.pyplot as plt


x = np.arange(0, 10, 0.2)
y = np.sin(x)
z = y
fig, ax = plt.subplots()


plt.title('Simplest default with labels')

ax.plot(x, y)
plt.show()
"""
import matplotlib.pyplot as plt
import numpy as np


def f(t):
    'A damped exponential'
    s1 = np.cos(2 * np.pi * t)
    e1 = np.exp(-t)
    return s1 * e1


t1 = np.arange(0.0, 5.0, .2)

l = plt.plot(t1, f(t1), 'ro')
#plt.setp(l, markersize=30)
#plt.setp(l, markerfacecolor='C0')

plt.show()