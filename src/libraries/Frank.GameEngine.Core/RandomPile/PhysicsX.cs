namespace Frank.GameEngine.Core.RandomPile;

public class PhysicsX
{
    public double g { get; set; }

    public double tInc { get; set; }

    public double time { get; set; }

    public S s { get; set; } = new();


    public double Height { get; set; }

    public double Width { get; set; }

    public double Length { get; set; }

    public double Z { get; set; }

    public double Y { get; set; }

    public double X { get; set; }

    public double Yb { get; set; }

    public double L { get; set; }

    public double Gamma { get; set; }

    public double Vm { get; set; }

    public double Alpha { get; set; }

    //----------------------------------------------------------------------------------------------------//
    // Initialize the global variables required for the simulation.
    //----------------------------------------------------------------------------------------------------//
    public void InitializeVariables()
    {
        Vm = 50d; // m/s
        Alpha = 25d; // degrees
        Gamma = 0d; // along x-axis
        L = 12d; // m
        Yb = 10d; // on x-z plane

        X = 400d; // m
        Y = 35d; // on x-z plane
        Z = 45d; // on x-axis
        Length = 10d; // m
        Width = 15d; // m
        Height = 20d; // m

        s.i = 0d; // m
        s.j = 0d; // m
        s.k = 0d; // m

        time = 0d; // seconds
        tInc = 0.05d; // seconds
        g = 9.8d; // m/(s*s)
    }

    //----------------------------------------------------------------------------------------------------//
    // This function steps the simulation ahead in time. This is where the kinematic properties
    // are calculated.  The function will return 1 when the target is hit, and 2 when the shell
    // hits the ground (x-z plane) before hitting the target, otherwise the function returns 0.
    //----------------------------------------------------------------------------------------------------//
    public int DoSimulation()
        //----------------------------------------------------------------------------------------------------//
    {
        double cosX;
        double cosY;
        double cosZ;
        double xe, ze;
        double b, Lx, Ly, Lz;
        double tx1, tx2, ty1, ty2, tz1, tz2;

        // step to the next time in the simulation
        time += tInc;

        // First calculate the direction cosines for the cannon orientation.
        // In a real game you would not want to put this calculation in this
        // function since it is a waste of CPU time to calculate these values
        // at each time step as they never change during the sim.  I only put them here in 
        // this case so you can see all the calculation steps in a single function.
        b = L * cos((90 - Alpha) * 3.14 / 180); // projection of barrel onto x-z plane
        Lx = b * cos(Gamma * 3.14 / 180); // x-component of barrel length
        Ly = L * cos(Alpha * 3.14 / 180); // y-component of barrel length
        Lz = b * sin(Gamma * 3.14 / 180); // z-component of barrel length

        cosX = Lx / L;
        cosY = Ly / L;
        cosZ = Lz / L;

        // These are the x and z coordinates of the very end of the cannon barrel
        // we'll use these as the initial x and z displacements
        xe = L * cos((90 - Alpha) * 3.14 / 180) * cos(Gamma * 3.14 / 180);
        ze = L * cos((90 - Alpha) * 3.14 / 180) * sin(Gamma * 3.14 / 180);

        // Now we can calculate the position vector at this time
        s.i = Vm * cosX * time + xe;
        s.j = Yb + L * cos(Alpha * 3.14 / 180) + Vm * cosY * time - 0.5 * g * time * time;
        s.k = Vm * cosZ * time + ze;

        // Check for collision with target
        // Get extents (bounding coordinates) of the target
        tx1 = X - Length / 2;
        tx2 = X + Length / 2;
        ty1 = Y - Height / 2;
        ty2 = Y + Height / 2;
        tz1 = Z - Width / 2;
        tz2 = Z + Width / 2;

        // Now check to see if the shell has passed through the target
        // I'm using a rudimentary collision detection scheme here where
        // I simply check to see if the shell's coordinates are within the
        // bounding box of the target.  This works for demo purposes, but
        // a practical problem is that you may miss a collision if for a given
        // time step the shell's change in position is large enough to allow 
        // it to "skip" over the target.
        // A better approach is to look at the previous time step's position data
        // and to check the line from the previous postion to the current position
        // to see if that line intersects the target bounding box.
        if (s.i >= tx1 && s.i <= tx2 &&
            s.j >= ty1 && s.j <= ty2 &&
            s.k >= tz1 && s.k <= tz2)
            return 1;

        // Check for collision with ground (x-z plane)
        if (s.j <= 0)
            return 2;

        // Cutoff the simulation if it's taking too long
        // This is so the program does not get stuck in the while loop
        if (time > 3600)
            return 3;

        return 0;
    }

    private double sin(double value)
    {
        return Math.Sin(value);
    }

    private double cos(double value)
    {
        return Math.Cos(value);
    }
}