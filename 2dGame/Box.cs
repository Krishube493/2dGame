using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace _2dGame
{
    class Box
    {
        public int x, y, size, size2;
        public string direction;
        public Image image;

        public Box(int _x, int _y, int _size, int _size2)
        {
            x = _x;
            y = _y;
            size = _size;
            size2 = _size2;
        }

        public Box(int _x, int _y, int _size, int _size2, string _direction, Image _image)
        {
            x = _x;
            y = _y;
            size = _size;
            size2 = _size2;
            direction = _direction;
            image = _image;
        }

        public void BoxMove(int speed, string direction)
        {
            //change the x of the boxes to move them over

            if (direction == "Right")
            {
                x += speed;
            }

            if (direction == "Left")
            {
                x -= speed;
            }
        }

        public void MoveBox(int speed)
        {
            //move down
            y += speed;
        }

        public void Move(int speed, string direction)
        {
            //to move player right or left 
            if (direction == "right")
            {
                if (x > 780)
                {

                }
                else
                {
                    x += speed;
                }
            }
            else if (direction == "left")
            {
                if (x < 10)
                {

                }
                else
                {
                    x -= speed;
                }
            }
        }

        public bool Collision(Box b)
        {
            Rectangle rec1 = new Rectangle(b.x, b.y, b.size, b.size);
            Rectangle rec2 = new Rectangle(x, y, size, size);

            //check for collision 
            if (rec1.IntersectsWith(rec2))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
