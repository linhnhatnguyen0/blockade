using Blockade

public class Case {
    
    private Wall topWall;
    private Wall rightWall;
    private Wall bottomWall;
    private Wall leftWall;

    private bool startingCase;

    public Case(bool startingCase) {
        
        this.startingCase = startingCase;
    }

    public bool hasTopWall() {

        return topWall != null;
    }

    public bool hasRightWall() {
        
        return rightWall != null;
    }

    private bool hasBottomWall() {
        
        return bottomWall != null;
    }

    private bool hasLeftWall() {
        
        return leftWall != null;
    }

    public bool StartingCase {

        get { return StartingCase; }
        
        set { StartingCase = value; }
    }
}