import math
#from tkinter import *
from random import random
from decimal import Decimal, getcontext

N=50                   #number of spins in the lattice
T = 2                 #temperature
iter = 3500000          #Number of iterations
rend = 5000             #rendering frequency
mfr = 5                #mean calculating frequency
nm = 599000


E = -2*(N**2)           #Total energy
M = -N**2                #Total magnetization
V = 0.001                #value

Em = 0                  #mean energy
Erm = 0                 #root mean energy

Mm = 0                  #mean magnetisation
Mrm = 0                 #root mean magnetisation

e1 = math.exp(-4 / T)
e2 = math.exp(-8 / T)

Sp_v = [[-1]*N for i in range(0,N)] 
getcontext().prec=10


#a=700//N   #rectangle width
#root = Tk()
#c = Canvas(root, width=800, height=800, bg='white')
#c.pack()

#def grid():  #grid for spins
#
#    for i in range(0,N+1):
#        c.create_line(50, 10+i*a, 750, 10+i*a) 
#
#    for i in range(0,N+1):
#        c.create_line(50+i*a, 10 , 50+i*a, 710)



#def spins_plot(): #spin sectangles
#
#    for i in range (0,N):
#        for j in range (0,N):
#            if Sp_v[i][j]==-1:
#                c.create_rectangle(51+i*a, 11+j*a, 64+i*a , 24+j*a, fill='red', outline='red',width=0)
#
#           if Sp_v[i][j]==1:
#                c.create_rectangle(51+i*a, 11+j*a, 64+i*a , 24+j*a, fill='#28F', outline='blue',width=0)


def rot(x,y):  #spin rotation
    #print(i1,j1)
    if (Sp_v[x][y] == 1):
        Sp_v[x][y] = -1
    else:
        Sp_v[x][y] = 1
    #print(d_E(i1,j1),d_E(i1,j1+1),d_E(i1+1,j1+1))


def d_E(x,y):  #energy for spin
    Exy = -Sp_v[x][y] * (( Sp_v[(x+1)%N][y] + Sp_v[(x-1)%N][y] + Sp_v[x][(y-1)%N] + Sp_v[x][(y+1)%N]))
    return Exy;

#def latt():    #redraw of the screen
#    c.delete(ALL)
#    spins_plot()
#    grid()
#    c.update()


Em_f = open('Em.txt','w')
Erm_f = open('Erm.txt','w')
Mm_f = open('Mm.txt','w')
Mrm_f = open('Mrm.txt','w')
k = 0


#print('lul1')
for w in range (0, 101):
    H = 0+w*0.06                  #temperature
    i = 0
    k = 0
    Em = 0                  #mean energy
    Erm = 0                 #root mean energy
    Mm = 0                  #mean magnetisation
    Mrm = 0                 #root mean magnetisation
    e1m_up = math.exp((-4+2*H) / T)
    e1m_d = math.exp((-4-2*H) / T)
    e2m_up = math.exp((-8+2*H) / T)
    e2m_d = math.exp((-8-2*H) / T)
    e1p_up = math.exp((+4+2*H) / T)
    e1p_d = math.exp((+4-2*H) / T)
    e2p_up = math.exp((+8+2*H) / T)
    e2p_d = math.exp((+8-2*H) / T)
    e0_up = math.exp((2*H) / T)
    e0_d = math.exp((-2*H) / T)
    print(H)

    for i in range(0,iter):
        x1 = math.floor(random()*N)
        y1 = math.floor(random()*N)
        r = random();

        if (Sp_v[x1][y1] == 1):
            r = r*math.exp(V*(4-4*M))
            if (((r < e0_d) and (d_E(x1,y1) == 0)) or ((r < e1m_d) and (d_E(x1,y1) == -2)) or ((r < e2m_d) and (d_E(x1,y1) == -4)) or ((r < e2p_d) and (d_E(x1,y1) == 4)) or ((r < e1p_d) and (d_E(x1,y1) == 2))):
               E = E - 2*d_E(x1,y1) + 2*H
               M = M - 2
               rot(x1,y1)
        else:
            r = r*math.exp(V*(4*M-4))
            if (((r < e0_up) and (d_E(x1,y1) == 0)) or ((r < e1m_up) and (d_E(x1,y1) == -2)) or ((r < e2m_up) and (d_E(x1,y1) == -4)) or ((r < e2p_up) and (d_E(x1,y1) == 4)) or ((r < e1p_up) and (d_E(x1,y1) == 2))):
               E = E - 2*d_E(x1,y1) - 2*H
               M = M + 2
               rot(x1,y1)

        if ((i+1)%500000 == 0):
            print(i+1,' E = ', E, ' M = ', M)

        if (i+1 > 500000):
            if (i%mfr == 0):
                Em = Em + (Decimal(E) / (N**2))
                Erm = Erm + (Decimal(E)**2 / (N**4))                 

                Mm = Mm + (Decimal(M) / (N**2))
                Mrm = Mrm + (Decimal(M)**2 / (N**4))
                k = k + 1

                if (k%nm == 0):
                    Em_f.write(str(Em/nm) + '\n')
                    Erm_f.write(str(Erm/nm) + '\n')
                    Mm_f.write(str(Mm/nm) + '\n')
                    Mrm_f.write(str(Mrm/nm) + '\n')
                    Em = Decimal(0)
                    Erm = Decimal(0)
                    Mm = Decimal(0)
                    Mrm = Decimal(0)


Em_f.close()
Erm_f.close()
Mm_f.close()
Mrm_f.close()
