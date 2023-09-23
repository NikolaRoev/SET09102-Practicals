using Test.Views;

namespace Test {
    public partial class AppShell : Shell {
        public AppShell() {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RolePage), typeof(RolePage));
        }
    }
}