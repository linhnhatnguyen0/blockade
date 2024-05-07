using Wall;

public class Case {
    private Wall topWall;
    private Wall leftWall;
    private Wall rightWall;
    private Wall bottomWall;
    private bool StartingCase;

    public Case(bool isBeginning){
        this.beginning = isBeginning;
    }

    public bool hasWall(){
        if(topWall == null) return true;
        if(leftWall == null) return true;
        if(rightWall == null) return true;
        if(bottomWall == null) return true;
        return false;
    }

    public bool hasPawn(){
        
    }

    public bool hasTopWall() {
        if(topWall == null)return false;
        return true;
    }

    public bool hasRightWall() {
        if(rightWall == null) return false;
        return true;
    }

    private bool hasBottomWall() {
        if (bottomWall == null) return false;
        return true;
    }

    private bool hasLeftWall() {
        if (leftWall == null) return false;
        return true;
    }
}