using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace OOP_LABA_6
{
    public partial class Main : Form
	{
		mystorage Storage = new mystorage();
		Pen pen = new Pen(Color.Red, 3);
		public Main()
        {
			InitializeComponent();
        }
		/// <summary>
		/// Группа может : Двигаться
		///				   Менять цвет
		///				   Менять размер
		///				   Отрисовываться 
		/// </summary>
		public abstract class FigureAbstract 
		{
			public virtual string save()
			{
				return "";
			}
			public  FigureAbstract _next;//указатель на некст объект
			public Color FigureIFColor = Color.Red;//цвет;
			public Color getColor()
			{
				return FigureIFColor;
			}
			public virtual void setnext(FigureAbstract obj)//метод set next
			{//задаем _next
				_next = obj;
			}
			public virtual FigureAbstract getnext() //метод возвращающий указатель не след.
			{//возвращает _next
				return _next;
			}
			public virtual void FactoryPaint(Panel paint_box, FigureAbstract figure, Pen pen) { }
			
			public virtual void moveX(int k) { }
			public virtual void moveY(int k) { }
			public virtual void changeSize(int k) { }
			public virtual int getX()
			{
				return 0;
			}
			public virtual int getY()
			{
				return 0;
			}

			public virtual void setColor(Color changeColor) { }
			public virtual void setX(int k) { }
			public virtual void setY(int k) { }
			public virtual bool checkFigure(FigureAbstract figure, MouseEventArgs e, Panel paint_box, Pen pen) 
			{
				return false;
			}
			public virtual int getsize() { return 0; }
		}
		public class factory : FigureAbstract
		{
			public virtual void FactoryPaintNEW(Panel paint_box, Pen pen, string code, string x, string y, string dlina)
			{
				int x1=Convert.ToInt32(x), y1=Convert.ToInt32(y), dlina1= Convert.ToInt32(dlina);
				if (code != null)
				{
					switch (code)
					{
						case "Line":
							FigureAbstract figure = new Line(x1, y1,dlina1);
							figure.FactoryPaint(paint_box, figure, pen);
							break;
						case "Сircle":
							FigureAbstract figure1 = new Сircle(x1, y1, dlina1);
							figure1.FactoryPaint(paint_box, figure1, pen);
							break;
						default:
							break;
					}
				}
			}
		}
		public class GroupFigures : FigureAbstract
		{
			public override string save()
			{
				string save1= "<> ";
				for (int i = 0; i < count; i++)
				{
					save1 += group[i].save();
				}
				save1 += " </> ";
				return save1;
			}
			public int maxcount = 10;
			int count;
			public FigureAbstract[] group; //public
			public GroupFigures(Color color)//конструктор с параметрами
			{
				FigureIFColor = color;
				count = 0;
				group = new FigureAbstract[maxcount];
			}
			public void GroupAddFigure(FigureAbstract newOBJECT)
			{
				if (count >= maxcount)
					return;

				count++;
				group[count - 1] = newOBJECT;
			}
			public override void setColor(Color changeColor) 
			{
				FigureIFColor = changeColor;
				for (int i = 0; i < count; i++)
				{
					group[i].setColor(changeColor);
				}
			}
			public override void setX(int k)
			{
				for (int i = 0; i < count; i++)
				{
					group[i].setX(k);
				}
			}
			public override void setY(int k)
			{
				for (int i = 0; i < count; i++)
				{
					group[i].setY(k);
				}
			}
			public override void moveX(int k) 
			{
				for (int i = 0; i < count; i++)
				{
					group[i].setX(k);
				}
			}
			public override void moveY(int k)
			{
				for (int i = 0; i < count; i++)
				{
					group[i].setY(k);
				}
			}
			public override bool checkFigure(FigureAbstract figure, MouseEventArgs e, Panel paint_box, Pen pen)
			{
				for (int i = 0; i < count; i++)
				{
					if (group[i].checkFigure(group[i], e, paint_box, pen))
					{
						this.FigureIFColor = Color.Orange;
						figure.FactoryPaint(paint_box, figure, pen);
						return true;
					}
				}
				return false;
			}
			public override void changeSize(int k) 
			{
				for (int i = 0; i < count; i++)
				{
					group[i].changeSize(k);
				}
			}
			public override void FactoryPaint(Panel paint_box, FigureAbstract figure, Pen pen) 
			{
				for (int i = 0; i < count; i++)
				{
					group[i].FigureIFColor = this.FigureIFColor;
					group[i].FactoryPaint(paint_box, group[i], pen);
				}
			}
		}


		public class FigureIF : FigureAbstract //класс фигур
		{
			private int x, y; // точки

			public FigureIF(int x, int y)//конструктор с параметрами
			{
				this.x = x;
				this.y = y;
			}
			public override int getX()
			{
				return x;
			}
			public override void setX(int k)
			{
				x+=k;
			}
			public override int getY()
			{
				return y;
			}
			public override void setY(int k)
			{
				y += k;
			}
			public override void setColor(Color changeColor)
			{
				FigureIFColor = changeColor;
			}
		}
		public void printFigure(FigureAbstract obj, Color color)//метод для отрисовки 
		{
			obj.setColor(color);
			obj.FactoryPaint(paint_box, obj, pen);
		}
		class Line : FigureIF
		{
			public override string save()
			{
				return "Line";
			}
			private int LineLength = 50; // длина линии
			public override int getsize() { return LineLength; }
			public Line(int x, int y) : base(x, y)//конструктор с параметрами
			{

			}
			public Line(int x, int y, int dlina) : base(x, y)//конструктор с параметрами
			{
				LineLength = dlina;
			}
			public int getLineLength()
			{
				return LineLength;
			}
			public void setLineLength(int LineLength)
			{
				this.LineLength=LineLength;
			}
			public override void FactoryPaint(Panel paint_box, FigureAbstract figure, Pen pen)
			{
				pen.Color = figure.getColor();//перекрываю метод, фабричного метода
				paint_box.CreateGraphics().DrawLine(pen, figure.getX() - getLineLength(),
					figure.getY(), figure.getX() + getLineLength(), figure.getY());
			}
			public override void changeSize(int k) 
			{
				LineLength += k;
			}
			public override void moveX(int k)
			{
				if (getX() - LineLength > 0 )
				{
					this.setX(k);
				}
				else
					this.setX(Math.Abs(k));
			}
			public override void moveY(int k)
			{
				if (getY()> 0)
				{
					this.setY(k);
				}
				else
					this.setY(Math.Abs(k));
			}
			public override bool checkFigure(FigureAbstract figure, MouseEventArgs e, Panel paint_box, Pen pen)
			{
				if (e.X >= figure.getX() - getLineLength()
					&& e.X <= figure.getX() + getLineLength()
					&& e.Y >= figure.getY() - 2
					&& e.Y <= figure.getY() + 2)
				{
					return true;
				}
				else return false;
			}
		}

		class Сircle : FigureIF
        {
			public override string save()
			{
				return "Сircle";
			}
			private int СircleRad = 30; //радиус круга
			public override int getsize() { return СircleRad; }
			public Сircle(int x, int y) : base(x,y)//конструктор с параметрами
            {

			}
			public Сircle(int x, int y, int dlina) : base(x, y)//конструктор с параметрами
			{
				СircleRad = dlina;
			}
			public int getRad()
			{
				return СircleRad;
			}
			public void setRad(int СircleRad)
			{
				this.СircleRad = СircleRad;
			}
			public override void FactoryPaint(Panel paint_box, FigureAbstract figure, Pen pen)
			{
				pen.Color = figure.getColor();//перекрываю метод фабричный
				paint_box.CreateGraphics().DrawEllipse(pen, figure.getX() - getRad(),
						figure.getY() - getRad(), getRad() * 2, getRad() * 2);
			}
			public override void changeSize(int k)
			{
				СircleRad += k;
			}
			public override void moveX(int k)
			{
				if (getX() - СircleRad > 0)
				{
					this.setX(k);
				}
				else
					this.setX(Math.Abs(k));
			}
			public override void moveY(int k)
			{
				if (getY() - СircleRad > 0)
				{
					this.setY(k);
				}
				else
					this.setY(Math.Abs(k));
			}
			public override bool checkFigure(FigureAbstract figure, MouseEventArgs e, Panel paint_box, Pen pen)
			{
				if ((figure.getX() - e.X) * (figure.getX() - e.X) +
					(figure.getY() - e.Y) * (figure.getY() - e.Y)
					<= getRad() * getRad())
				{
					return true;
				}
				else return false;
			}
		}

		public class mystorage
		{
			private FigureAbstract head;//головной узел
			private FigureAbstract next;//следующий узел
			private FigureAbstract end;//последний узел
			private int N;//кол-во
			public mystorage()
			{//конструктор
				head = null;// список пустой
				N = 0;//кол-во
			}
			public int getN()
			{
				return N;
			}
			public void add(FigureAbstract obj)
			{//добавляем новый объект
				next = null;
				if (head == null)//если первый раз добавляем
					head = obj;
				else
					end.setnext(obj);
				end = obj;
				N++;//добавляем число объектов
			}

			public FigureAbstract First()
			{
				next = head;//присваиваю next головному узлу
				return next;
			}

			public FigureAbstract Next()
			{
				next = next.getnext();//присваиваю next след.
				return next;
			}

			public bool EOL()
			{//проверка на пустоту
				if (next == null)
					return false;
				return true;
			}
			public FigureAbstract getobj()
			{//возвращаю next
				return next;
			}

			public void delobj(FigureAbstract selobj)
			{//удаление объекта
				First();
				FigureAbstract back = head;
				while (EOL())//пока не конец
				{
					if (next == selobj)//если находим selobj
						break;
					back = next;//
					Next();
				}
				if (selobj == head || selobj == end)//передаем все свойства объекта перед удалением 
				{
					if (selobj == head)//если это головной 
					{
						head = head.getnext();
						back = head;
					}
					if (selobj == end)//если это последний 
					{
						end = back;
						if (end != null)
							end.setnext(null);
					}
				}
				else
					back.setnext(selobj.getnext());

				selobj = null;
				N--;
			}

			public void adddown(FigureAbstract obj, FigureAbstract select)
			{
				FigureAbstract nex = select.getnext();
				if (nex != null)
					obj.setnext(nex);
				select.setnext(obj);
				if (end == select)
					end = obj;
				this.N++;
			}
		}

		public bool clickCheck(MouseEventArgs e)
		{
			bool check = false;
			for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
			{
				if (!ModifierKeys.HasFlag(Keys.Control) && Storage.getobj().getColor() == Color.Orange)
				{//если не нажат ctrl и цвет объекта оранжевый
					printFigure(Storage.getobj(), Color.Red);
				}
			}

			for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
			{
				if (Storage.getobj().checkFigure(Storage.getobj(), e, paint_box, pen))
				{
					printFigure(Storage.getobj(), Color.Orange);
					check = true;
				}
			}
			return check;
		}

		private void paint_box_MouseClick(object sender, MouseEventArgs e)
		{
			
			if (clickCheck(e)) //проверка 
			{
				return;
			}
			FigureIF obj; // создание объекта
			if (radioButton1.Checked == true)
			{
				obj = new Сircle(e.X, e.Y);//если нажимаю на пустое место - создаю круг
			}
			else
			{
				obj = new Line(e.X, e.Y);//если нажимаю на пустое место - создаю линию
			}

			printFigure(obj, Color.Orange); //метод printFigure
			label_paintbox.Visible = false;

			Storage.add(obj);//добавляю в хранилище
		}

		private void Main_KeyDown(object sender, KeyEventArgs e)
		{
			int k = 5;
			if (e.KeyCode.Equals(Keys.A) || e.KeyCode.Equals(Keys.D))
			{ //если нажал влево или вправо 
				for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
				{
					if (Storage.getobj().getColor() == Color.Orange && e.KeyCode.Equals(Keys.A))
						Storage.getobj().moveX(-k); //если влево
					else if(Storage.getobj().getColor() == Color.Orange)
						Storage.getobj().moveX(k); //если вправо
				}
				paint_box.Refresh();
				for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
					Storage.getobj().FactoryPaint(paint_box, Storage.getobj(), pen);
			}
			else if (e.KeyCode.Equals(Keys.W) || e.KeyCode.Equals(Keys.S))
			{ //если нажал вверх или вниз
				for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
				{
					if (Storage.getobj().getColor() == Color.Orange && e.KeyCode.Equals(Keys.W))
						Storage.getobj().moveY(-k); //если вверх
					else if(Storage.getobj().getColor() == Color.Orange)
						Storage.getobj().moveY(k); //если вниз
				}
				paint_box.Refresh();
				for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
					Storage.getobj().FactoryPaint(paint_box, Storage.getobj(), pen);
			}
			else if(e.KeyCode.Equals(Keys.Add) || e.KeyCode.Equals(Keys.Subtract))
			{ //если нажал + или -
				for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
				{
					if (Storage.getobj().getColor() == Color.Orange && e.KeyCode.Equals(Keys.Add))
						Storage.getobj().changeSize(k);
					else if (Storage.getobj().getColor() == Color.Orange)
						Storage.getobj().changeSize(-k);
				}
				paint_box.Refresh();
				for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
					Storage.getobj().FactoryPaint(paint_box, Storage.getobj(), pen);
			}
			else if (e.KeyCode.Equals(Keys.Delete))
			{ //если нажал delete
				for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
				{
					if (Storage.getobj().getColor() == Color.Orange)
					{
						Storage.delobj(Storage.getobj());//удаляю объект выделенный
						continue;
					}
				}
				paint_box.Refresh();
				for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
				{
					Storage.getobj().FactoryPaint(paint_box, Storage.getobj(), pen);
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (colorDialog1.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
				textBox4.BackColor = colorDialog1.Color; //изменение цвета
		}

		private void textBox4_Click(object sender, EventArgs e)
		{
			for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
			{
				if (Storage.getobj().getColor() == Color.Orange)
				{
					Storage.getobj().setColor(((TextBox)sender).BackColor);
				} //изменение цвета
			}
			paint_box.Refresh();
			for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
			{
				Storage.getobj().FactoryPaint(paint_box, Storage.getobj(), pen);
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			GroupFigures newOBJECT = new GroupFigures(Color.Orange);
			bool check = true;
			for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
			{
				if(Storage.getobj().getColor() == Color.Orange)
				{
					newOBJECT.GroupAddFigure(Storage.getobj());
					if (check)
					{
						Storage.adddown(newOBJECT, Storage.getobj());
						Storage.delobj(Storage.getobj());
						check = false;
						Storage.Next();
						continue;
					}
					Storage.delobj(Storage.getobj()); 
					
				}
			}
		}

		private void output_Click(object sender, EventArgs e)
		{
			paint_box.Refresh();
			const Int32 BufferSize = 128;
			using (StreamWriter streamWriter = new StreamWriter(path))
			{
				streamWriter.WriteLine(Storage.getN());
				for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
				{
					streamWriter.WriteLine(Storage.getobj().save());
					streamWriter.WriteLine(Storage.getobj().getX());
					streamWriter.WriteLine(Storage.getobj().getY());
					streamWriter.WriteLine(Storage.getobj().getsize());
				}
			}

		}

		string path = @"C:\OOP_LABA7\filename.txt";

		private void input_Click(object sender, EventArgs e)
		{
			String smth,code="", x="", y="", dlina=""; int count = 0, count1 =0;
			const Int32 BufferSize = 128;
			using (var fileStream = File.OpenRead(path))
			using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, BufferSize))
			{
				count1 = Convert.ToInt32(streamReader.ReadLine());
				count1 = count1*4+1;
				while (count1 != 0)
				{
					if(count >3)
   					{
						count = 0;
						factory fact = new factory();
						fact.FactoryPaintNEW(paint_box, pen, code, x, y, dlina);
						continue;
					}
					smth = streamReader.ReadLine();
					if (count == 0)
						code = smth;
					else if(count == 1)
						x = smth;
					else if(count == 2)
						y = smth;
					else if(count == 3)
						dlina = smth;
					
					count++;
					count1--;
				}
			}
		}
	}
}
