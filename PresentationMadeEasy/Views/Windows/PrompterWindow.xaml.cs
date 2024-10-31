using Microsoft.UI.Windowing;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using System.Collections.ObjectModel;
using PresentationMadeEasy.Models;
using PresentationMadeEasy.Enums;
using Microsoft.UI.Text;
using Gma.System.MouseKeyHook;

namespace PresentationMadeEasy.Views.Windows
{
    public sealed partial class PrompterWindow : Window
    {
        private OverlappedPresenter? _presenter;
        private IKeyboardMouseEvents GlobalHook;
        private readonly ObservableCollection<Content> Contents;
        private int currentPage = 0;

        public PrompterWindow(ObservableCollection<Content> contents)
        {
            InitializeComponent();

            InitializeWindow();

            _presenter = AppWindow.Presenter as OverlappedPresenter;

            Contents = contents;

            SetContent();

            SetEnabledStates();
        }

        private void InitializeHook()
        {
            GlobalHook = Hook.GlobalEvents();

            GlobalHook.MouseDown += GlobalHook_MouseDown;
        }

        private void Unsubscribe()
        {
            try
            {
                GlobalHook.MouseDown -= GlobalHook_MouseDown;

                GlobalHook.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        private void GlobalHook_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            Navigate(NavigationDirection.Next);
        }

        private void InitializeWindow()
        {
            ExtendsContentIntoTitleBar = true;

            SetTitleBar(AppTitleBar);

            AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Collapsed;

            AppWindow.Resize(new SizeInt32(540, 270));

            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Win32Interop.GetWindowIdFromWindow(hWnd);

            AppWindow.SetIcon("Assets/Logo/logo.ico");

            if (this.AppWindow != null && this.AppWindow.Presenter is OverlappedPresenter presenter)
            {
                presenter.IsMaximizable = false;
                presenter.IsResizable = true;
                presenter.IsAlwaysOnTop = true;
            }
        }

        private void SetContent()
        {
            TitleTextBlock.Text = $"{Contents[currentPage].Id}. {Contents[currentPage].Title}";
            ParagraphRichEdit.Text = Contents[currentPage].Paragraph;
        }

        private void PreviousButton_Click(object sender, RoutedEventArgs e)
        {
            Navigate(NavigationDirection.Previous);
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            Navigate(NavigationDirection.Next);
        }

        private void Navigate(NavigationDirection direction)
        {
            switch (direction)
            {
                case NavigationDirection.Next:
                    if (currentPage < Contents.Count - 1)
                        currentPage++;

                    break;
                case NavigationDirection.Previous:
                    if (currentPage > 0)
                        currentPage--;

                    break;
            }

            SetContent();

            SetEnabledStates();
        }

        private void SetEnabledStates()
        {
            // next button
            if (currentPage == Contents.Count - 1)
            {
                NextButton.IsEnabled = false;
            } else
            {
                NextButton.IsEnabled = true;
            }

            // previous button
            if (currentPage == 0)
            {
                PreviousButton.IsEnabled = false;
            }
            else
            {
                PreviousButton.IsEnabled = true;
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Unsubscribe();

            MainWindow mw = new MainWindow(Contents);

            mw.Activate();

            Close();
        }

        private void RecordMouseButton_Checked(object sender, RoutedEventArgs e)
        {
            ToolTipService.SetToolTip(RecordMouseButton, "Fare dinleyicisini kapat");
            InitializeHook();
        }

        private void RecordMouseButton_Unchecked(object sender, RoutedEventArgs e)
        {
            ToolTipService.SetToolTip(RecordMouseButton, "Fare dinleyicisini aç");
            Unsubscribe();
        }
    }
}
