using System;

namespace ODESolver;

struct Functs{
	public static double X_prime(double t, double x) => Consts.Lambda*x;
	public static double Y_prime(double t, double y, double z) => Consts.Omega*z;
	public static double Z_prime(double t, double y, double z) => -Consts.Omega*y;
	public struct Analytical{
		public static double X(double t) => Consts.InitialConditions.X * Math.Pow(Math.E, Consts.Lambda*t);
		public static double Y(double t) => Consts.InitialConditions.Z * Math.Sin(Consts.Omega*t);
		public static double Z(double t) => Consts.InitialConditions.Z * Math.Cos(Consts.Omega*t);
	}
	public static bool Convergence() => Math.Abs(1 + Consts.Lambda*Consts.IntegrationSteps.Euler) <= 1;
}
