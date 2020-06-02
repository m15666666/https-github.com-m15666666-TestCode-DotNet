import numpy as np
from scipy import stats

import pandas
data = pandas.read_csv('examples/brain_size.csv', sep=';', na_values=".")


a = np.ones((3, 3))

print(a)