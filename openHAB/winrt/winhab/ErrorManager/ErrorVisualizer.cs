using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Popups;

namespace openhab.winrt.winhab.ErrorManager
{
    /// <summary>
    /// Displays an Error
    /// </summary>
    public class ErrorVisualizer
    {
        public static ErrorVisualizer errorVisualizerInsance { get; private set; }

        //IErrorController errorController { get; set; }
        //public ErrorVisualizer(IErrorController errorController)
        //{
        //    this.errorController = errorController;
        //    ErrorVisualizer.errorVisualizerInsance = this;
        //}



        public async void showError(String stackTrace, String message)
        {
            //Show Toast, if there was an Error
            ////ToastTemplateType toastTemplate = ToastTemplateType.ToastImageAndText01;
            ////XmlDocument toastXML = ToastNotificationManager.GetTemplateContent(toastTemplate);
            ////XmlNodeList toastTextElements = toastXML.GetElementsByTagName("text");
            ////toastTextElements[0].AppendChild(toastXML.CreateTextNode("An Error occured, while setting up the Connection" + stackTrace));
            ////XmlNodeList toastImageElements = toastXML.GetElementsByTagName("image");
            ////((XmlElement)toastImageElements[0]).SetAttribute("src", "ms-appx:///Assets/images/icon.png");
            ////((XmlElement)toastImageElements[0]).SetAttribute("alt", "red graphic");
            ////ToastNotification toast = new ToastNotification(toastXML);
            //ToastNotificationManager.CreateToastNotifier().Show(toast);
            //await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            // {
            //     //Use this function to access the GUI, from another Thread
            //     MessageDialog dialog = new MessageDialog("Fehler", "Error while sending a request");
            //     //MessageDialog dialog = new MessageDialog("aaaaa", "Error while sending a request");
            //     //dialog.Options = MessageDialogOptions.AcceptUserInputAfterDelay;
            //     try
            //     {
            //         dialog.ShowAsync();
            //     }
            //     catch (Exception ex)
            //     {
            //         int x = 0;
            //     }
            // });
    

        }

    }
}
