
from keras.models import load_model
from keras.preprocessing import image
import numpy as np
import sys


def prediction:
	
	photo = sys.argv[1]
	modelFile = sys.argv[2]

    model = load_model(modelFile)

    img = image.load_img(photo, target_size=(256, 256))
    x = image.img_to_array(img)
    x = np.expand_dims(x, axis=0)
    x = np.divide(x, 255)

    scores = model.predict(x)
    result = np.argmax(scores)

    return(result)

input1 = input()
input2 = input()
result = prediction(input1, input2)
print(result)
