using System;

namespace ODESolver;

struct Functs{
	public static double Y_prime(double y) => Consts.Lambda*y;
	public static double Analytical(double t) => Consts.InitialConditions.Y * Math.Pow(Math.E, Consts.Lambda*t);
	public static bool Convergence() => Math.Abs(1 + Consts.Lambda*Consts.IntegrationSteps.Euler) <= 1;
}
