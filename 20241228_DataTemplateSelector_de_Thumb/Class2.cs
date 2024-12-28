using System.Windows.Controls;
using System.Windows;

namespace _20241228_DataTemplateSelector_de_Thumb
{
  
    public class MyDTSelector : DataTemplateSelector
    {
        public DataTemplate? DT1 { get; set; }
        public DataTemplate? DT2 { get; set; }
        public DataTemplate? DT3 { get; set; }

        /// <summary>
        /// 今回の場合だと、引数のitemにMyDataが入っているので、
        /// データタイプを識別してそれぞれに合ったDataTemplateを返している
        /// それぞれのDataTemplateの設定はXAMLの方で行っている
        /// </summary>
        /// <param name="item"></param>
        /// <param name="container"></param>
        /// <returns></returns>
        public override DataTemplate? SelectTemplate(object item, DependencyObject container)
        {

            if (item is not MyData)
            {
                return base.SelectTemplate(item, container);
            }
            else if (item is MyData dd)
            {
                if (dd.Type == ThumbType.Text) { return DT1; }
                else if (dd.Type == ThumbType.Ellipse) { return DT2; }
                else if (dd.Type == ThumbType.Rect) { return DT3; }
                else { return base.SelectTemplate(item, container); }
            }
            return base.SelectTemplate(item, container);
        }
    }
}
