using System.Windows;
using System.Windows.Media;

namespace toolstray
{
    public static class VisualExtensions
    {
        public static T GetDescendantByType<T>(this Visual element) where T : Visual
        {
            if (element == null)
            {
                return null;
            }
            if (element is T ele)
            {
                return ele;
            }
            T foundElement = null;
            if (element is FrameworkElement)
            {
                (element as FrameworkElement).ApplyTemplate();
            }
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(element); i++)
            {
                Visual visual = VisualTreeHelper.GetChild(element, i) as Visual;
                foundElement = visual.GetDescendantByType<T>();
                if (foundElement != null)
                {
                    break;
                }
            }
            return foundElement;
        }
    }
}