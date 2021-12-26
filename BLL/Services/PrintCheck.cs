using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using BLL.Models;
using BLL.Interfaces;
using System.Collections.ObjectModel;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;
using iTextSharp.text.pdf.draw;

namespace BLL.Services
{
    public class PrintCheck: IPrintCheck
    {
        IDbRepos db;
        public PrintCheck(IDbRepos dbRepository)
        {
            db = dbRepository;
        }

        public void GetCheck(Order_Model order)
        {
            string file = @"Check.pdf";
            var customer = db.Customers.GetItem((int)order.customer_id);
            var lines = db.Order_lines.GetList().Where(i => i.order_id == order.order_id).Join(db.Products.GetList(), i => i.inventory_number, pr => pr.inventory_number, (i, pr) => new { i.order_line_cost, i.number, pr.product_name, pr.product_quantity }).ToList();
            var points = db.Puck_Up_Points.GetItem((int)order.pick_up_point_id);

            FileStream fs = new FileStream(file, FileMode.Create);
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);

            BaseFont baseFont = BaseFont.CreateFont("arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font font1 = new Font(baseFont, 14, Font.BOLD);
            Font font = new Font(baseFont, 12);

            document.Open();

            Paragraph header = new Paragraph("Кассовый чек", font1);
            header.Alignment = Element.ALIGN_CENTER;
            Paragraph date = new Paragraph($"Дата оформления заказа: {order.date}", font);
            Paragraph name = new Paragraph($"Заказчик: {customer.customer_name}", font);
            Paragraph point = new Paragraph($"{points.pick_up_point_name}", font);
            Paragraph ord = new Paragraph($"Статус: {db.Statuses.GetItem((int)order.status_id).status_name}", font);
            Paragraph separator = new Paragraph("***********************************************************************************", font1);
            separator.Alignment = Element.ALIGN_CENTER;
            Chunk b = new Chunk(new VerticalPositionMark());
            Paragraph har = new Paragraph($"Наименование: ", font);
            har.Add(new Chunk(b));
            har.Add("Цена");

            document.Add(header);
            document.Add(new Paragraph("\n"));
            document.Add(date);
            document.Add(name);
            document.Add(point);
            document.Add(new Paragraph("\n"));
            document.Add(ord);
            document.Add(new Paragraph("\n"));
            document.Add(separator);
            document.Add(har);

            foreach (var i in lines)
            {
                Paragraph prod = new Paragraph($"{i.product_name}: {i.number} шт.", font);
                prod.Add(new Chunk(b));
                prod.Add($"{i.order_line_cost}руб.");
                document.Add(prod);
                document.Add(new Paragraph("\n"));
            }

            document.Add(new Paragraph("\n"));
            document.Add(separator);

            Chunk h = new Chunk(new VerticalPositionMark());
            Paragraph Cost = new Paragraph($"Итого:", font1);
            Cost.Add(new Chunk(h));
            Cost.Add($"{order.total_cost} руб.");
            document.Add(Cost);
            document.Add(separator);

            document.Close();
            writer.Close();
            fs.Close();

            Process iStartProcess = new Process();
            iStartProcess.StartInfo.FileName = file;
            iStartProcess.Start();
        }

        public void GetReport(ObservableCollection<Order_Model> orders, int UserId, DateTime CostsDate1, DateTime CostsDate2)
        {
            string file = @"Report.pdf";
            var customer = db.Customers.GetItem(UserId);
            //var lines = db.Order_lines.GetList().Where(i => i.order_id == order.order_id).Join(db.Products.GetList(), i => i.inventory_number, pr => pr.inventory_number, (i, pr) => new { i.order_line_cost, i.number, pr.product_name, pr.product_quantity }).ToList();
            //var points = db.Puck_Up_Points.GetItem((int)order.pick_up_point_id);

            FileStream fs = new FileStream(file, FileMode.Create);
            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(document, fs);

            BaseFont baseFont = BaseFont.CreateFont("arial.ttf", BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            Font font1 = new Font(baseFont, 14, Font.BOLD);
            Font font = new Font(baseFont, 12);

            document.Open();

            Paragraph header = new Paragraph("Отчет расходов", font1);
            header.Alignment = Element.ALIGN_CENTER;
            Paragraph Date = new Paragraph($"с {CostsDate1} по {CostsDate2}", font1);
            Date.Alignment = Element.ALIGN_CENTER;
            //Paragraph date = new Paragraph($"Дата оформления заказа: {order.date}", font);
            Paragraph name = new Paragraph($"Клиент: {customer.customer_name}", font);
            //Paragraph point = new Paragraph($"{points.pick_up_point_name}", font);
           // Paragraph s = new Paragraph($"Статус: {db.Statuses.GetItem((int)order.status_id).status_name}", font);
            Paragraph separator = new Paragraph("***********************************************************************************", font1);
            separator.Alignment = Element.ALIGN_CENTER;
            Chunk b = new Chunk(new VerticalPositionMark());
           // Paragraph har = new Paragraph($"Наименование: ", font);
            //har.Add(new Chunk(b));
            //har.Add("Цена");

            document.Add(header);
            document.Add(Date);
            document.Add(new Paragraph("\n"));
            document.Add(name);
            document.Add(separator);
            //document.Add(date);
            //document.Add(point);
            //document.Add(new Paragraph("\n"));
            //document.Add(ord);
            //document.Add(new Paragraph("\n"));
            //document.Add(separator);
            //document.Add(har);
            decimal totalsale = 0;
            decimal totalcost = 0;
            int totalnumber = 0;
            foreach (var i in orders)
            {
                int Number = 0;
                var lines = db.Order_lines.GetList().Where(j => j.order_id == i.order_id).ToList();
                foreach(var line in lines)
                {
                    Number += line.number;
                }
                decimal sale = customer.sale * i.total_cost/100;
                var points = db.Puck_Up_Points.GetItem((int)i.pick_up_point_id);
                Paragraph d = new Paragraph($"Дата:{i.date}", font);
                Paragraph n = new Paragraph($"Количество товаров:{Number} шт.", font);
                Paragraph p = new Paragraph($"Пункт выдачи:{points.pick_up_point_name}", font);
                Paragraph sl = new Paragraph($"Скидка:{sale} руб.", font);
                Paragraph c = new Paragraph($"Цена:{i.total_cost} руб.", font);
                totalsale += sale;
                totalcost += i.total_cost;
                totalnumber += Number;
                
                document.Add(d);
                document.Add(n);
                document.Add(p);
                document.Add(sl);
                document.Add(c);
                document.Add(new Paragraph("\n\n"));
            }

            document.Add(new Paragraph("\n"));
            document.Add(separator);

            Paragraph v = new Paragraph($"Общая сумма расходов за период: {totalcost} руб.", font1);
            Paragraph S = new Paragraph($"Общая сумма скидки: {totalsale} руб.", font1);
            Paragraph o = new Paragraph($"Количество купленных товаров : {totalnumber} шт.", font1);

            document.Add(v);
            document.Add(S);
            document.Add(o);

            document.Add(separator);

            document.Close();
            writer.Close();
            fs.Close();

            Process iStartProcess = new Process();
            iStartProcess.StartInfo.FileName = file;
            iStartProcess.Start();
        }
    }
}
