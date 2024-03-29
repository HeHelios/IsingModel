from Grid import Field
from graphics_core import init, latt
from random import randint, random
from math import exp


def MonteCarlo(grid, iterationNumber, temperature, magneticField = 0, show = False, showFrequency = 100000):

    width = grid.width
    height = grid.height
    
    for i in range(iterationNumber):
        x = randint(0, width-1)
        y = randint(0, height-1)
        
        energyChange = grid.FlipEnergy(x, y, magneticField)
        
        if energyChange <= 0:
            grid.FlipSpin(x, y, magneticField)
        
        elif random() <= exp(-energyChange / temperature):
            grid.FlipSpin(x, y, magneticField)

        if show and (i % showFrequency == 0):
            latt(grid)
    
        
if __name__ == '__main__':
    
    print('start')
    g = Field(100, 100)
    init(100, 100)
    MonteCarlo(g, 10000000, 4, show = True)
    print('finish')