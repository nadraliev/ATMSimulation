using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ATMSimulation
{
    /// <summary>
    /// Interaction logic for QueuePage.xaml
    /// </summary>
    public partial class QueuePage : Page
    {
        public QueuePage()
        {
            InitializeComponent();




        }

        double QueueStartY;
        double QueueX;
        double QueueEndY;
        double QueueLastY;
        int customerImageHeight = 68;

        Customer currentCustomer;
        Image currentCustomerImage;


        Queue<Customer> customersQueue = new Queue<Customer>();
        Queue<Image> customersImageQueue = new Queue<Image>();
        Timer queueTimer = new Timer(9000);
        Timer customerEnteractionTimer = new Timer(5000);


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            QueueStartY = atm.Margin.Top + atm.ActualHeight;
            QueueEndY = ActualHeight;
            QueueX = ActualWidth / 2 - customerImageHeight / 2;
            QueueLastY = QueueStartY;



            queueTimer.AutoReset = true;
            queueTimer.Elapsed += Timer_Elapsed;
            queueTimer.Interval = 1;
            queueTimer.Start();
            queueTimer.Interval = 10000;

            customerEnteractionTimer.AutoReset = false;
            customerEnteractionTimer.Elapsed += CustomerEnteractionTimer_Elapsed;


        }

        private void CustomerEnteractionTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            QueueLastY = QueueStartY;
            currentCustomer = null;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                currentCustomerImage.BeginAnimation(MarginProperty, GetCustomerLeaveAnimation());
                MoveQueue();
            }));
            customerEnteractionTimer.Interval += 500;
            
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {

                if (currentCustomer == null)    //first customer
                {
                    currentCustomer = new Customer();
                    currentCustomerImage = GetNewCustomerImage();

                    QueueLastY += customerImageHeight + 10;

                    roomGrid.Children.Add(currentCustomerImage);
                    currentCustomerImage.BeginAnimation(MarginProperty, GetCustomerMoveToATMAnimation());
                }
                else if (customersQueue.Count < 4)  //rest of the queue
                {
                    Image image = GetNewCustomerImage();
                    Customer customer = new Customer();

                    customersQueue.Enqueue(customer);
                    customersImageQueue.Enqueue(image);

                    roomGrid.Children.Add(image);
                    image.BeginAnimation(MarginProperty, GetCustomerMoveForwardAnimation());

                    QueueLastY += customerImageHeight + 10;

                    
                }
            }));
            if (queueTimer.Interval > 1000)
                queueTimer.Interval = queueTimer.Interval - 1000;

        }

        private void MoveQueue()
        {
            int count = customersQueue.Count;
            for (int i = 0; i < count; i++)
            {
                Customer customer = customersQueue.Dequeue();
                Image image = customersImageQueue.Dequeue();
                if (i == 0)
                {
                    currentCustomer = customer;
                    currentCustomerImage = image;
                    image.BeginAnimation(MarginProperty, GetCustomerMoveToATMAnimation());
                    QueueLastY = customerImageHeight + 10 + QueueStartY;
                }
                else
                {
                    customersQueue.Enqueue(customer);
                    customersImageQueue.Enqueue(image);
                    image.BeginAnimation(MarginProperty, GetCustomerMoveForwardAnimation());
                    QueueLastY = QueueStartY + (i+1) * (customerImageHeight + 10);
                }
            }
            
        }

        Image GetNewCustomerImage()
        {
            Image newCustomerImage;
            newCustomerImage = new Image();
            newCustomerImage.Height = 68;
            BitmapImage spooderman = new BitmapImage();
            spooderman.BeginInit();
            spooderman.UriSource = new Uri("Resources/spooderman.png", UriKind.Relative);
            spooderman.EndInit();
            newCustomerImage.Source = spooderman;
            newCustomerImage.HorizontalAlignment = HorizontalAlignment.Left;
            newCustomerImage.VerticalAlignment = VerticalAlignment.Top;
            newCustomerImage.Margin = new Thickness(QueueX, QueueEndY, 0, 0);
            return newCustomerImage;
        }

        ThicknessAnimation GetCustomerMoveForwardAnimation()
        {
            ThicknessAnimation newCustomerAnimation;
            newCustomerAnimation = new ThicknessAnimation();
            newCustomerAnimation.To = new Thickness(QueueX, QueueLastY + 10, 0, 0);
            newCustomerAnimation.Duration = TimeSpan.FromMilliseconds(3000);
            return newCustomerAnimation;
        }

        ThicknessAnimation GetCustomerMoveToATMAnimation()
        {
            ThicknessAnimation customerMoveToATMAnimation;
            customerMoveToATMAnimation = new ThicknessAnimation();
            customerMoveToATMAnimation.To = new Thickness(QueueX, QueueStartY, 0, 0);
            customerMoveToATMAnimation.Duration = TimeSpan.FromMilliseconds(3000);
            customerMoveToATMAnimation.Completed += CustomerMoveToATMAnimation_Completed;
            return customerMoveToATMAnimation;
        }

        ThicknessAnimation GetCustomerLeaveAnimation()
        {
            ThicknessAnimation leaveAnimation = new ThicknessAnimation();
            leaveAnimation.To = new Thickness(-100, QueueStartY, 0, 0);
            leaveAnimation.Duration = TimeSpan.FromMilliseconds(3000);
            return leaveAnimation;
        }

        private void CustomerMoveToATMAnimation_Completed(object sender, EventArgs e)
        {
            customerEnteractionTimer.Start();
        }
    }
}
