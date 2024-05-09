namespace Blockade
{
    public class Case
    {

        private Wall topWall;
        private Wall rightWall;
        private Wall bottomWall;
        private Wall leftWall;

        private bool startingCase;

        public Case()
        {

            this.startingCase = false;
        }

        public bool hasTopWall()
        {

            return topWall != null;
        }

        public bool hasRightWall()
        {

            return rightWall != null;
        }

        public bool hasBottomWall()
        {

            return bottomWall != null;
        }

        public bool hasLeftWall()
        {

            return leftWall != null;
        }

        public bool StartingCase
        {

            get { return startingCase; }

            set { startingCase = value; }
        }

        public Wall TopWall
        {

            get { return topWall; }

            set { topWall = value; }
        }

        public Wall RightWall
        {

            get { return rightWall; }

            set { rightWall = value; }
        }

        public Wall LeftWall
        {

            get { return leftWall; }

            set { leftWall = value; }
        }

        public Wall BottomWall
        {

            get { return bottomWall; }

            set { bottomWall = value; }
        }
    }
}

