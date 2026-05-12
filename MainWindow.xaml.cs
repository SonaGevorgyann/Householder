using System;
using System.Data;
using System.Linq;
using System.Windows;
using HouseholderSolver.Logic;

namespace HouseholderSolver
{
    public partial class MainWindow : Window
    {
        private DataTable _matrixTable;
        private DataTable _vectorTable;

        public MainWindow()
        {
            InitializeComponent();
        }

       
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(SizeBox.Text, out int n) || n < 1)
            {
                MessageBox.Show("Enter valid matrix size");
                return;
            }

            // MATRIX A
            _matrixTable = new DataTable();

            for (int c = 0; c < n; c++)
                _matrixTable.Columns.Add($"C{c + 1}", typeof(double));

            for (int r = 0; r < n; r++)
            {
                var row = _matrixTable.NewRow();
                for (int c = 0; c < n; c++)
                    row[c] = 0.0;

                _matrixTable.Rows.Add(row);
            }

            MatrixGrid.ItemsSource = _matrixTable.DefaultView;

            // VECTOR b
            _vectorTable = new DataTable();
            _vectorTable.Columns.Add("b", typeof(double));

            for (int r = 0; r < n; r++)
            {
                var row = _vectorTable.NewRow();
                row[0] = 0.0;
                _vectorTable.Rows.Add(row);
            }

            VectorGrid.ItemsSource = _vectorTable.DefaultView;

            ResultGrid.ItemsSource = null;
            ResidualText.Text = "";
            DeterminantText.Text = "";
        }

      
        private void Solve_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_matrixTable == null || _vectorTable == null)
                {
                    MessageBox.Show("Create matrix first");
                    return;
                }

                double[][] A = GetMatrix();
                double[] b = GetVector();

                double[] x = HouseholderSolverLogic.Solve(A, b);

                double residual = HouseholderSolverLogic.ComputeResidual(A, x, b);
                double det = HouseholderSolverLogic.ComputeDeterminant(A);

                ResidualText.Text = $"Residual Error: {residual:F10}";
                DeterminantText.Text = $"Det(A) = {det:F6}";

                ShowResult(x);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

      
        private double[][] GetMatrix() //read matrix from datatable
        {
            int n = _matrixTable.Rows.Count;
            double[][] A = new double[n][];

            for (int i = 0; i < n; i++)
            {
                A[i] = new double[n];

                for (int j = 0; j < n; j++)
                {
                    A[i][j] = Convert.ToDouble(_matrixTable.Rows[i][j]);
                }
            }

            return A;
        }

       
        private double[] GetVector() //read vector from datatable
        {
            int n = _vectorTable.Rows.Count;
            double[] b = new double[n];

            for (int i = 0; i < n; i++)
            {
                b[i] = Convert.ToDouble(_vectorTable.Rows[i][0]);
            }

            return b;
        }

        private void ShowResult(double[] x)
        {
            DataTable result = new DataTable();
            result.Columns.Add("x", typeof(double));

            foreach (var xi in x)
            {
                result.Rows.Add(Math.Round(xi, 6));
            }

            ResultGrid.ItemsSource = result.DefaultView;
        }
    }
}