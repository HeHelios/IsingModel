class Field:
    def __init__(self, width, height):
        if (not isinstance(width, int)):
            raise ValueError("The width must be an int")
        if (not isinstance(height, int)):
            raise ValueError("The height must be an int")
        
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
    
    '''
    def SpinFlip(x, y):
        startEnergy = self.field[()]
    '''  
    
    def DrawField(self):
        print(self.field)

if __name__ == '__main__':
    field = Field(5, 5)
    field.SetCell(0, 2, -1)
    print(field.SumNeighbours(4, 2))