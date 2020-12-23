﻿using System;
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
		public mystorage Storage;
		Pen pen = new Pen(Color.Red, 3);
		TreeViewer newtree;

		public Main()
        {
			InitializeComponent();
			newtree = new TreeViewer(this);
			Storage = new mystorage();
			Storage.addObserver(newtree);
		}
		public class FigureAbstract : ICloneable
		{
			public virtual string save()
			{
				return "";
			}
			private FigureAbstract _next;//указатель на некст объект
			protected Color FigureIFColor = Color.Orange;//цвет;
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

			public virtual void setColor(Color changeColor) 
			{
				FigureIFColor = changeColor;
			}
			public virtual void setX(int k) { }
			public virtual void setY(int k) { }
			public virtual bool checkFigure(FigureAbstract figure, MouseEventArgs e, Panel paint_box, Pen pen) 
			{
				return false;
			}
			public virtual int getsize() { return 0; }
			public object Clone()
			{
				return this.MemberwiseClone();
			}
		}
		public class factory : FigureAbstract
		{
			public virtual FigureAbstract FactoryPaintNEW(Panel paint_box, Pen pen, string code, string x, string y, string dlina)
			{
				int x1=Convert.ToInt32(x), y1=Convert.ToInt32(y), dlina1= Convert.ToInt32(dlina);
				
				FigureAbstract figure;
				switch (code)
				{
					case "Line":
						figure = new Line(x1, y1,dlina1);
						figure.FactoryPaint(paint_box, figure, pen);
						return figure;
					case "Сircle":
						figure = new Сircle(x1, y1, dlina1);
						figure.FactoryPaint(paint_box, figure, pen);
						return figure;
				}
				return null;

			}
		}
		public class GroupFigures : FigureAbstract
		{
			public override string save()
			{
				string save1= "<>\n";
				for (int i = 0; i < count; i++)
				{
					save1 += group[i].save() + "\n";
				}
				save1 += "</>";
				return save1;
			}
			private int maxcount = 10;
			public int count;
			public FigureAbstract[] group; //public
			public GroupFigures()//конструктор с параметрами
			{
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
				this.FigureIFColor = changeColor;
				for (int i = 0; i < count; i++)
				{
					group[i].setColor(changeColor);
				}
			}
			public override void setX(int k)
			{//
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
					group[i].moveX(k);
				}
			}
			public override void moveY(int k)
			{
				for (int i = 0; i < count; i++)
				{
					this.group[i].moveY(k);
				}
			}
			public override bool checkFigure(FigureAbstract figure, MouseEventArgs e, Panel paint_box, Pen pen)
			{
				for (int i = 0; i < count; i++)
				{
					if (group[i].checkFigure(group[i], e, paint_box, pen))
					{
						this.setColor(Color.Orange);
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
					group[i].setColor(this.getColor());
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
				string objec = "";
				objec += "Line\n" + getX() + "\n"+ getY() + "\n" + getsize();
				return objec;
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
				if (getY() > 0)
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
				string objec = "";
				objec += "Сircle\n" + getX() + "\n" + getY() + "\n" + getsize();
				return objec;
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

		public class Observer
        {
			public virtual void onSubjectChanged(mystorage who) { }
        }
		public class LipkiiObserver : Observer
        {

        }
		
		public class TreeViewer : Observer
		{
			TreeView tree;
			public TreeViewer(Form form)
            {
				tree = new TreeView();
				tree.Location = new System.Drawing.Point(721, 310);
				tree.Size = new System.Drawing.Size(208, 300);
				tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tree_AfterSelect);
				form.Controls.Add(tree);
			}
			public void tree_AfterSelect(object sender, TreeViewEventArgs e)//Выделение через дерево
			{
				if (e.Node.Parent != null && e.Node.Parent.Text != "Фигуры")
				{
					selectOBj(tree, e.Node.Parent);
				}
			}
			public void selectOBj(TreeView tree, TreeNode tn)
			{
				if (tn.Parent != null && tn.Parent.Text != "Фигуры")
				{
					selectOBj(tree, tn.Parent);
				}
				else
				{
					int index = tn.Parent.Index, check = 0;
                    //for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
                    //{
                    //    if (check == index)
                    //    {
                    //        printFigure(Storage.getobj(), Color.Orange);
                    //        continue;
                    //    }
                    //    printFigure(Storage.getobj(), Color.Red);
                    //    check++;
                    //}
                }
			}
			public void treeSelect(int index)
            {
				tree.SelectedNode = tree.Nodes[0].Nodes[index];
				tree.Focus();
			}
			public void treeClear()
            {
				tree.Nodes.Clear();
			}
			public override void onSubjectChanged(mystorage who)
            {
				tree.Nodes.Clear();
				tree.Nodes.Add("Фигуры");
				for (who.First(); who.EOL(); who.Next())//нашли next = obj 
				{
					processNode(tree.Nodes[0], who.getobj());
				}
			}
			public void processNode(TreeNode tn, FigureAbstract o)
			{
				char[] MyChar = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '\n' };
				string name = o.save().TrimEnd(MyChar);
				if (name.Length > 6)
					name = "Group";

				TreeNode node = tn.Nodes.Add(name);
				node.EnsureVisible();
				if (o is GroupFigures)
				{
					for (int i = 0; i < (o as GroupFigures).count; i++)
					{
						processNode(node, (o as GroupFigures).group[i]);
					}
				}

			}
		}
		
		public class CStorage : ICloneable
		{
			List<Observer> observers;
			public CStorage()
			{
				observers = new List<Observer>();
			}
			public void addObserver(Observer o)
            {
				observers.Add(o);
            }
			public void notifyEveryone(mystorage stor)
            {
                foreach (var item in observers)
                {
                    item.onSubjectChanged(stor);
                }
            }
			public object Clone()
			{
				return this.MemberwiseClone();
			}
		}
		public class mystorage : CStorage 
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
			public void setN(int k) 
			{
				this.N = k;
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
				mystorage stor = (mystorage)this.Clone();
				notifyEveryone(stor);
			}
			
			//(Person) p1.Clone();
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
			public FigureAbstract getEND()
			{//возвращаю next
				return end;
			}
			public void delobj(FigureAbstract selobj)
			{//удаление объекта
				int delcount = 1;
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

				if (selobj is GroupFigures)
				{
					delcount--;
					delcounter(selobj as GroupFigures, ref delcount);
				}
				selobj = null;
				N-= delcount;

				mystorage stor = (mystorage)this.Clone();
				notifyEveryone(stor);
			}
			public void delcounter(GroupFigures group, ref int count)
			{
				for (int i = 0; i < group.count; i++)
				{
					if (group.group[i] is GroupFigures)
					{
						count--;
						delcounter(group.group[i] as GroupFigures, ref count);
					}
					count++;
				}
			}
			public void adddown(FigureAbstract obj, FigureAbstract select)
			{
				FigureAbstract nex = select.getnext();
				if (nex != null)
					obj.setnext(nex);
				select.setnext(obj);
				if (end == select)
					end = obj;

				mystorage stor = (mystorage)this.Clone();
				notifyEveryone(stor);
			}
		}

		public bool clickCheck(MouseEventArgs e, TreeViewer currentTree)
		{
			bool check = false;
			for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
			{
				if (!ModifierKeys.HasFlag(Keys.Control) && Storage.getobj().getColor() == Color.Orange)
				{//если не нажат ctrl и цвет объекта оранжевый
					printFigure(Storage.getobj(), Color.Red);
				}
			}
			int countTree = 0;
			for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
			{
				if (Storage.getobj().checkFigure(Storage.getobj(), e, paint_box, pen))
				{
					currentTree.treeSelect(countTree);
					printFigure(Storage.getobj(), Color.Orange);
					check = true;
				}
				countTree++;
			}
			return check;
		}

		private void paint_box_MouseClick(object sender, MouseEventArgs e)
		{
			
			if (clickCheck(e,newtree)) //проверка 
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
			int count = 0;
			for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
			{
				if (Storage.getobj() == obj)
                {
					newtree.treeSelect(count);
					return;
                }
				count++;
			}
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
		private void textBox3_Click(object sender, EventArgs e)
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
			int n = Storage.getN();
			groupAdder();
			Storage.setN(n);
		}

		public void groupAdder()
        {
			GroupFigures newOBJECT = new GroupFigures();
			bool check = true;
			int count = 0, countTree = 0;
			for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
			{
				if (Storage.getobj().getColor() == Color.Orange)
				{
					newOBJECT.GroupAddFigure(Storage.getobj()); //Storage.setN(1);
					if (check)
					{
						Storage.adddown(newOBJECT, Storage.getobj());
						countTree = count;
						Storage.delobj(Storage.getobj());
						check = false;
						Storage.Next();
						continue;
					}
					Storage.delobj(Storage.getobj());
				}
				count++;
			}
			newtree.treeSelect(countTree);
		}
		private void output_Click(object sender, EventArgs e)
		{
			newtree.treeClear();
			paint_box.Refresh();
			using (StreamWriter streamWriter = new StreamWriter(path))
			{
				streamWriter.WriteLine(Storage.getN());
				for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
				{
					streamWriter.WriteLine(Storage.getobj().save());
				}
			}

		}

		string path = @"C:\OOP_LABA7\filename.txt";

		private void input_Click(object sender, EventArgs e)
		{
			Storage = new mystorage();
			Storage.addObserver(newtree);
			paint_box.Refresh();
			String smth,code="", x="", y="", dlina=""; int countFigures = 0;
			using (StreamReader streamReader = new StreamReader(path))
			{
				factory fact = new factory();
				countFigures = Convert.ToInt32(streamReader.ReadLine());
				countFigures = countFigures * 4;
				while (countFigures > 0)
				{
					smth = streamReader.ReadLine();
					if (smth == "<>")
					{
						Stack<string> stack = new Stack<string>();
						Stack<string> stkFinal = new Stack<string>();
						List<FigureAbstract> list = new List<FigureAbstract>();
						stack.Push("<>");
                        while (stack.Count != 0)
                        {
							smth = streamReader.ReadLine();
							if (smth == "</>")
							{
								GroupFigures newOBJECT = new GroupFigures();
								while (stack.Peek() != "<>")
								{
									stkFinal.Push(stack.Peek());
									stack.Pop();
								}
								stack.Pop();
								while (stkFinal.Count != 0)
								{
									if (stkFinal.Peek() == "group")
									{
										newOBJECT.GroupAddFigure(list[list.Count - 1]);
										list.RemoveAt(list.Count - 1);
										stkFinal.Pop();
										continue;
									}
									code = stkFinal.Peek(); stkFinal.Pop();
									x = stkFinal.Peek(); stkFinal.Pop();
									y = stkFinal.Peek(); stkFinal.Pop();
									dlina = stkFinal.Peek(); stkFinal.Pop();

									newOBJECT.GroupAddFigure(fact.FactoryPaintNEW(paint_box, pen, code, x, y, dlina));
									Storage.setN(Storage.getN() + 1);
								}
								if (stack.Count == 0)
								{
									Storage.add(newOBJECT);
									Storage.setN(Storage.getN() - 1);
									break;
								}
								else
								{
									stack.Push("group");
									list.Add(newOBJECT);
								}
							}
							else
							{
								countFigures--;
								stack.Push(smth);
							}
						}
					}
                    else
                    {
						Storage.add(fact.FactoryPaintNEW(paint_box, pen, smth, streamReader.ReadLine()
							, streamReader.ReadLine(), streamReader.ReadLine()));
						countFigures -= 4;
					}
						
				}
			}
		}
	}
}
