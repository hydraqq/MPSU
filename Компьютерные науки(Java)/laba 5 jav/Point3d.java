public class Point3d {
    private double xCoord;
    private double yCoord;
    private double zCoord;
    public Point3d(double x, double y, double z) {
        xCoord = x;
        yCoord = y;
        zCoord = z;
    }

    public Point3d() {
        this(0.0, 0.0, 0.0);
    }

    public double getX() {
        return xCoord;
    }
    
    public double getY() {
        return yCoord;
    }
    
    public double getZ() {
        return zCoord;
    }
    
    public void setX(double val) {
        xCoord = val;
    }
    
    public void setY(double val) {
        yCoord = val;
    }
    
    public void setZ(double val) {
        zCoord = val;
    }
    
    public boolean equals(Point3d other) {
        return (this.xCoord == other.xCoord) && 
               (this.yCoord == other.yCoord) && 
               (this.zCoord == other.zCoord);
    }
    
    public double distanceTo(Point3d other) {
        double dx = this.xCoord - other.xCoord;
        double dy = this.yCoord - other.yCoord;
        double dz = this.zCoord - other.zCoord;
        
        double distance = Math.sqrt(dx*dx + dy*dy + dz*dz);

        return Math.round(distance * 100.0) / 100.0;
    }
}