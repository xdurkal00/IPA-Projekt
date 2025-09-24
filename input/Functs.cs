using System;

namespace ODESolver;

struct Functs{
	public static double Y_prime(double z) => Consts.Omega*z;
	public static double Z_prime(double y) => -Consts.Omega*y;
	public static double Analytical_y(double t) => Consts.InitialConditions.Z * Math.Sin(Consts.Omega*t);
	public static double Analytical_z(double t) => Consts.InitialConditions.Z * Math.Cos(Consts.Omega*t);
	public static bool Convergence() => Math.Abs(1 + Consts.Lambda*Consts.IntegrationSteps.Euler) <= 1;
}
