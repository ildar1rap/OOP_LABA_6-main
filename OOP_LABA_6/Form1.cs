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
		public class FigureIF //класс фигур
		{
			private int x, y; // точки
			private Color FigureIFColor = Color.Red;//цвет;
			private FigureIF _next;//указатель на некст объект
			public FigureIF(int x, int y)//конструктор с параметрами
			{
				this.x = x;
				this.y = y;
			}
			public int getX()
			{
				return x;
			}
			public void setX(int k)
			{
				x+=k;
			}
			public int getY()
			{
				return y;
			}
			public void setY(int k)
			{
				y += k;
			}
			public Color getColor()
			{
				return FigureIFColor;
			}
			public void setColor(Color changeColor)
			{
				FigureIFColor = changeColor;
			}
			public void setnext(FigureIF obj)//метод set next
			{//задаем _next
				_next = obj;
			}
			public FigureIF getnext() //метод возвращающий указатель не след.
			{//возвращает _next
				return _next;
			}
			public virtual void FactoryPaint(Panel paint_box, FigureIF figure, Pen pen) 
			{ } //фабричный метож
			public virtual void moveX(int k)
			{
				x += k;
			}
			public virtual void moveY(int k)
			{
				y += k;
			}
			public virtual void changeSize(int k) { }
		}
		public void printFigure(FigureIF obj, Color color)//метод для отрисовки 
		{
			obj.setColor(color);
			obj.FactoryPaint(paint_box, obj, pen);
		}
		class Line : FigureIF
		{
			private int LineLength = 50; // длина линии
			public Line(int x, int y) : base(x, y)//конструктор с параметрами
			{

			}
			public int getLineLength()
			{
				return LineLength;
			}
			public override void FactoryPaint(Panel paint_box, FigureIF figure, Pen pen)
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
		}

		class Сircle : FigureIF
        {
			private int СircleRad = 30; //радиус круга
            public Сircle(int x, int y) : base(x,y)//конструктор с параметрами
            {

            }
			public int getRad()
			{
				return СircleRad;
			}
			public override void FactoryPaint(Panel paint_box, FigureIF figure, Pen pen)
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
		}

		class mystorage
		{
			private FigureIF head;//головной узел
			private FigureIF next;//следующий узел
			private FigureIF end;//последний узел
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
			public void add(FigureIF obj)
			{//добавляем новый объект
				next = null;
				if (head == null)//если первый раз добавляем
					head = obj;
				else
					end.setnext(obj);
				end = obj;
				N++;//добавляем число объектов
			}

			public FigureIF First()
			{
				next = head;//присваиваю next головному узлу
				return next;
			}

			public FigureIF Next()
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
			public FigureIF getobj()
			{//возвращаю next
				return next;
			}

			public FigureIF getobj(FigureIF obj)
			{//возвращаю next, если 
				for (First(); EOL(); Next())//нашли next = obj
					if (next == obj)
						return next;
				return null;//иначе выводим NULL
			}

			public void delobj(FigureIF selobj)
			{//удаление объекта
				First();
				FigureIF back = head;
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
		}

		public bool clickCheck(MouseEventArgs e)
		{
			for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
			{
				if (!ModifierKeys.HasFlag(Keys.Control) && Storage.getobj().getColor() == Color.Orange)
				{//если не нажат ctrl и цвет объекта оранжевый
					printFigure(Storage.getobj(), Color.Red);
				}
			}

			bool check = false;
			for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
			{
				if(Storage.getobj() is Line //если это линия и условие нажатие на линию
					&& e.X >= Storage.getobj().getX() - ((Line)Storage.getobj()).getLineLength()
					&& e.X <= Storage.getobj().getX() + ((Line)Storage.getobj()).getLineLength()
					&& e.Y >= Storage.getobj().getY() - 2
					&& e.Y <= Storage.getobj().getY() + 2)
				{
					printFigure(Storage.getobj(), Color.Orange);
					check = true;
				}
				if (Storage.getobj() is Сircle && //если это круг и условие нажатие на круг
					(Storage.getobj().getX() - e.X )* (Storage.getobj().getX() - e.X )+
					(Storage.getobj().getY() - e.Y) * (Storage.getobj().getY() - e.Y) 
					<= ((Сircle)Storage.getobj()).getRad() * ((Сircle)Storage.getobj()).getRad())
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
			FigureIF krug; // создание объекта
			if (radioButton1.Checked == true)
			{
				krug = new Сircle(e.X, e.Y);//если нажимаю на пустое место - создаю круг
			}
			else
			{
				krug = new Line(e.X, e.Y);//если нажимаю на пустое место - создаю линию
			}

			printFigure(krug, Color.Orange); //метод printFigure
			label_paintbox.Visible = false;

			Storage.add(krug);//добавляю в хранилище
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

	}
}
