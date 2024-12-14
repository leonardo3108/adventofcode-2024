'''
The problem is about finding the fewest number of seconds that must elapse for the robots to display a Christmas tree.
we can use the same code to simulate the movement of the robots, but we need to find the time when the robots are arranged in a picture of a Christmas tree.
At any time, we can search for a pattern that looks like a Christmas tree in the grid of robots. 
If we find the pattern, we just need to print the time when the pattern is found.

How to manually find the pattern of a Christmas tree:
1. Run the simulation for 101 x 103 = 10303 seconds.
2. Print the grid representation of the robots at each second in a bitmap image.
3. Open the images and look for a pattern of clustering in x coordinate. We have found the pattern at 23 seconds.
4. Look for a pattern of clustering in y coordinate. We have found the pattern at 89 seconds.
5. These patterns ocurred every 101 seconds, for the x coordinate, and every 103 seconds, for the y coordinate.
6. Find the image where both patterns are found at the same time. So, we have to find the first collision of the two patterns: 23, 124, 225... and 89, 192, 295...
7. Created a python script to find the first collision of the two patterns. The script is in the file "find.py". It found the first collision at 7093 seconds.
8. Verify the pattern at 7093 seconds. The pattern is a Christmas tree. So 7093 is the awnser.
'''


def findColision(x, y, dx, dy, xx, yy):
    while (True):
        x += dx
        if x in yy:
            return x, 0
        xx.append(x)
        y += dy
        if y in xx:
            return 0, y
        yy.append(y)

xx = [23]
yy = [89]
x, y = findColision(23, 89, 101, 103, xx, yy)
if y == 0:
    print('x', x, ' - ', x - 23, ',', x - 89, ' - ', (x - 23) // 101, ',', (x - 89) // 103)

else:
    print('y', y, ' - ', y - 23, ',', y - 89, ' - ', (x - 23) // 101, ',', (x - 89) // 103)

