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
using Windows.UI;
using Windows.Graphics;
using System.Collections.ObjectModel;
using PresentationMadeEasy.Models;
using Microsoft.UI.Text;
using System.Threading.Tasks;
using PresentationMadeEasy.Views.Windows;

namespace PresentationMadeEasy
{
    public sealed partial class MainWindow : Window
    {
        private OverlappedPresenter? _presenter;
        public ObservableCollection<Content> Contents { get; set; }
        private Content CurrentEditingContent;
        private bool isEditing = false;
        private bool isReadOnly = false;

        public MainWindow()
        {
            InitializeComponent();

            InitializeWindow();

            Contents = new ObservableCollection<Content>();

            ParagraphsListView.ItemsSource = Contents;

            _presenter = AppWindow.Presenter as OverlappedPresenter;
        }

        public MainWindow(ObservableCollection<Content> _contents) : this()
        {
            Contents = _contents;

            ParagraphsListView.ItemsSource = Contents;
        }

        private void InitializeWindow()
        {
            ExtendsContentIntoTitleBar = true;

            SetTitleBar(AppTitleBar);

            AppWindow.TitleBar.BackgroundColor = Colors.Transparent;
            AppWindow.TitleBar.ButtonForegroundColor = Colors.Black;
            AppWindow.TitleBar.ButtonHoverBackgroundColor = Colors.Transparent;
            AppWindow.TitleBar.ButtonHoverForegroundColor = Colors.Black;
            AppWindow.TitleBar.ButtonPressedBackgroundColor = new Color() { R = 249, G = 157, B = 136, A = 255 };

            this.AppWindow.Resize(new SizeInt32(1280, 720));

            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Win32Interop.GetWindowIdFromWindow(hWnd);

            AppWindow.SetIcon("Assets/Logo/logo.ico");

            if (this.AppWindow != null && this.AppWindow.Presenter is OverlappedPresenter presenter)
            {
                presenter.IsMaximizable = true;
                presenter.IsResizable = true;
                presenter.IsAlwaysOnTop = false;
            }
        }

        private void AddContentButton_Click(object sender, RoutedEventArgs e)
        {
            ParagraphEditBox.Document.GetText(TextGetOptions.None, out string docValue);

            if (isEditing)
            {
                foreach(var item in Contents)
                {
                    if (item.Id == CurrentEditingContent.Id)
                    {
                        if (!string.IsNullOrEmpty(TitleTextBox.Text))
                            item.Title = TitleTextBox.Text;

                        if (!string.IsNullOrEmpty(docValue))
                            item.Paragraph = GetEditBoxText();
                    }
                }

                AddContentButtonFontIcon.Glyph = "\uECC8";
                AddContentButtonTextBlock.Text = "Ekle";

                isEditing = false;

                RootGrid.Focus(FocusState.Programmatic);
                ClearInputAreas();
                ShowInfoBar("Basarili", "Icerik basariyla kaydedildi!", InfoBarSeverity.Success);

                return;
            }

            if (Equals(docValue, "\r"))
                docValue = null;

            if (string.IsNullOrEmpty(docValue) || string.IsNullOrEmpty(TitleTextBox.Text))
            {
                ShowInfoBar("Bilgi", "Lutfen bos alanlari doldurun", InfoBarSeverity.Informational);
                return;
            }

            Content content = new Content();

            content.Title = TitleTextBox.Text;
            content.Paragraph = GetEditBoxText();

            foreach (var item in Contents)
            {
                if (item.Title.ToLower() == content.Title.ToLower())
                {
                    ShowInfoBar("Uyari", "Zaten boyle bir bolum var! Farkli bir baslik vermeyi deneyin", InfoBarSeverity.Warning);
                    return;
                }
            }

            content.Id = Contents.Count + 1;

            Contents.Add(content);

            ClearInputAreas();
        }

        private void ClearInputAreas()
        {
            TitleTextBox.Text = "";
            ParagraphEditBox.Document.SetText(TextSetOptions.None, "");
        }

        private void ShowInfoBar(string title, string message, InfoBarSeverity severity)
        {
            FieldInfoBar.Title = title;
            FieldInfoBar.Message = message;
            FieldInfoBar.Severity = severity;

            FieldInfoBar.IsOpen = true;
        }

        private void DeleteContentButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            var content = button.DataContext as Content;

            Contents.Remove(content);
            UpdateIds();
        }

        private void EditContentButton_Click(object sender, RoutedEventArgs e)
        {
            isEditing = true;

            var button = sender as Button;

            var content = button.DataContext as Content;

            CurrentEditingContent = content;

            //if (content.Title == TitleTextBox.Text)
            //{
            //    var result = await CreateDialog("Kaydedilmemiş değişiklikler var", "Kaydet", "Kaydetme", "İptal").ShowAsync();

            //    if (result == ContentDialogResult.Primary)
            //    {

            //    }
            //}

            AddContentButtonFontIcon.Glyph = "\uE792";
            AddContentButtonTextBlock.Text = "Kaydet";

            TitleTextBox.Text = content.Title;
            SetEditBoxText(content.Paragraph);
        }

        private string GetEditBoxText()
        {
            ParagraphEditBox.Document.GetText(TextGetOptions.None, out string result);

            return result;
        }

        private void SetEditBoxText(string text)
        {
            ParagraphEditBox.Document.SetText(TextSetOptions.None, text);
        }

        private void UpdateIds()
        {
            if (Contents.Count == 0)
                return;

            int id = 1;

            foreach (var item in Contents)
            {
                item.Id = id;
                id++;
            }
        }

        private ContentDialog CreateDialog(string title, string primaryText, string? secondaryText, string cancelText)
        {
            ContentDialog dialog = new ContentDialog();

            dialog.XamlRoot = RootGrid.XamlRoot;
            dialog.Title = title;
            dialog.PrimaryButtonText = primaryText;

            if (secondaryText != null)
                dialog.SecondaryButtonText = secondaryText;

            dialog.CloseButtonText = cancelText;
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = new Content();

            return dialog;
        }
        private void ParagraphsListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        private void PrompterWindowButton_Click(object sender, RoutedEventArgs e)
        {
            ShowPrompterWindow();
        }

        private void ShowPrompterWindow()
        {
            if (Contents.Count > 0)
            {
                PrompterWindow pw = new PrompterWindow(Contents);
                pw.Activate();
                Close();
            } else
            {
                ShowInfoBar("Hata", "Sunum yardimcisini baslatmak icin yeterli icerik yok", InfoBarSeverity.Error);
            }
        }
    }
}
