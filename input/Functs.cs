using System;

namespace ODESolver;

struct Functs{
	public static double X_prime(double t, double x) => Consts.Lambda*x;
	public static double Y_prime(double t, double y, double z) => Consts.Omega*z;
	public static double Z_prime(double t, double y, double z) => -Consts.Omega*y;
	public static Matrix<double> A = DenseMatrix.OfArray(new double[,]{
			{0d, Consts.Omega_c, 0d},
			{-Consts.Omega_c, 0d, 0d},
			{0d, 0d, 0d}});
	public struct Analytical{
		public static double X(double t) => Consts.InitialConditions.X * Math.Pow(Math.E, Consts.Lambda*t);
		public static double Y(double t) => Consts.InitialConditions.Z * Math.Sin(Consts.Omega*t);
		public static double Z(double t) => Consts.InitialConditions.Z * Math.Cos(Consts.Omega*t);
		public static double V_x(double t) => Consts.InitialConditions.V_x * Math.Cos(Consts.Omega_c*t) + Consts.InitialConditions.V_y * Math.Sin(Consts.Omega_c*t);
		public static double V_y(double t) => -Consts.InitialConditions.V_x * Math.Sin(Consts.Omega_c*t) + Consts.InitialConditions.V_y * Math.Cos(Consts.Omega_c*t);
		public static double V_z(double t) => Consts.InitialConditions.V_z;
	}
	public static bool Convergence() => Math.Abs(1 + Consts.Lambda*Consts.IntegrationSteps.Euler) <= 1;
}
