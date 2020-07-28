from math import floor
from tkinter import *

Na = 0
Na = 0
a = 0
c = 0
root = 0

def init(x, y, width = 800, height = 800, bg='white'):
    
    global Na, Nb, a, root, c
    Na = x
    Nb = y
    a = floor((min(width, height) - 100) / max(Na, Nb))
    root = Tk()    
    c = Canvas(root, width=width, height=height, bg='white')
    c.pack()


def grid():  #grid for spins
    
    for i in range(0,Na+1):
        c.create_line(50, 10+i*a, 750, 10+i*a) 

    for i in range(0,Nb+1):
        c.create_line(50+i*a, 10 , 50+i*a, 710)


def spins_plot(grid): #spin sectangles

    for x, y in grid:
            if grid.GetCell(x, y) == -1:
                c.create_rectangle(51+x*a, 11+y*a, 64+x*a , 24+y*a, fill='red', outline='red',width=0)

            if grid.GetCell(x, y) == 1:
                c.create_rectangle(51+x*a, 11+y*a, 64+x*a , 24+y*a, fill='#28F', outline='blue',width=0)


def latt():    #redraw of the screen
    c.delete(ALL)
    spins_plot()
    grid()
    c.update()

if __name__ == '__main__':
    pass