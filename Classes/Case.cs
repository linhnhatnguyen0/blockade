using Blockade

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
        if(topWall == null) return false;
        if(leftWall == null) return false;
        if(rightWall == null) return false;
        if(bottomWall == null) return false;
        return true;
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