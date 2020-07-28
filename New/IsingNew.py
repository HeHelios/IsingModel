class Field:
    def __init__(self, width, height):
        if (not isinstance(width, int)):
            raise ValueError("The width must be an int")
        if (not isinstance(height, int)):
            raise ValueError("The height must be an int")
        
        self.field = {}
        
        for x in range(0, width):
            for y in range(0, height):
                temp = (x, y)
                self.field[temp] = -1
    
    def DrawField(self):
        print(self.field)

class Main:
    field = Field(3, 5)
    Field.DrawField(field)
    