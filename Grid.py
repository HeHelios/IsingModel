class Field:
    def __init__(self, width, height):
        #Field size initialization
        self.width = width
        self.height = height
        
        #Field properties initialization
        self.energy = 4 * width * height
        self.magnetization = width * height
        
        #Field initializateion
        self.field = {}
        
        for x in range(0, width):
            for y in range(0, height):
                temp = (x, y)
                self.field[temp] = 1
    
    def __iter__(self):   
        return self.field.__iter__()
    
    def __str__(self):
        line = "" 
            
        for x in range(0, self.width):
            for y in range(0, self.height):
                line += str(self.GetCell(x, y)) + " "
            line += '\n'
        
        return line
        
    def GetCell(self, x, y):
        if (x >= self.width):
            x = 0
        if (y >= self.height):
            y = 0
        if (x < 0):
            x = self.width - 1
        if (y < 0):
            y = self.height - 1
            
        return self.field[(x, y)]
     
    def SetCell(self, x, y, value):
        self.field[(x, y)] = value
    
    def SumNeighbours(self, x, y):
        return self.GetCell(x, y+1) + self.GetCell(x, y-1) + self.GetCell(x-1, y) + self.GetCell(x+1, y)
    
    def SpinFlip(self, x, y):
        energyChange = (-2) * self.GetCell(x, y) * self.SumNeighbours(x, y)
        
        self.energy += energyChange
        self.magnetization -= 2*self.GetCell(x, y)
        self.SetCell(x, y, -self.GetCell(x, y))
        
        return energyChange

if __name__ == '__main__':
    field = Field(5, 5)
    print(field)
    print(field.energy)
    print(field.SpinFlip(4, 4))
    print(field.energy)
    print(field.SpinFlip(3, 4))
    print(field.energy)
    print(field.SpinFlip(2, 4))
    print(field.energy)
    print(field.SpinFlip(3, 4))
    print(field.energy)
    print(field)
    print(field.GetCell(2, 4))