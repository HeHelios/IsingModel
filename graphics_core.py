from math import floor
from tkinter import *
from Grid import Field

Na = 0
Nb = 0
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
        c.create_line(50+i*a, 50 , 50+i*a, 50 + Nb*a) 

    for i in range(0,Nb+1):
        c.create_line(50, 50+i*a, 50 + Na * a, 50+i*a)


def spins_plot(grid): #spin sectangles

    for x, y in grid:
        if grid.GetCell(x, y) == -1:
            col = 'red'
        else:     
            col = '#28F'
                
        c.create_rectangle(51+x*a, 51+y*a, 51+(x+1)*a , 51+(y+1)*a, fill=col, outline=col ,width=0)


def latt(g):    #redraw of the screen
    c.delete(ALL)
    spins_plot(g)
    grid()
    c.update()

if __name__ == '__main__':
    x = 20
    y = 10
    g = Field(x, y)
    g.SpinFlip(2,1)
    init(x, y)
    latt(g)