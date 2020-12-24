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
		mystorage Storage;
		Pen pen = new Pen(Color.Red, 3);
		TreeViewer newtree;
		public Main()
		{
			InitializeComponent();
			newtree = new TreeViewer(this, selectOBj);
			Storage = new mystorage();
			Storage.addObserver(newtree);
		}
		public class FigureAbstract : ICloneable
		{
			private FigureAbstract _next;//указатель на некст объект
			protected Color FigureIFColor = Color.Orange;//цвет;
			public Color getColor()
			{
				return FigureIFColor;
			}
			public virtual string save()
			{
				return "";
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
			
			List<Observer> observersFigure = new List<Observer>();
			public void addObserver(Observer o)
			{
				observersFigure.Add(o);
			}
			public void notifyEveryone(FigureAbstract obj)
			{
				foreach (var item in observersFigure)
				{
					item.onSubjectChanged(obj);
				}
			}
			public object Clone()
			{
				return this.MemberwiseClone();
			}
		}
		public class factory : FigureAbstract
		{
			public virtual FigureAbstract FactoryPaintNEW(Panel paint_box, Pen pen, string code, string x, string y, string dlina)
			{
				FigureAbstract figure;
				switch (code)
				{
					case "Line":
						figure = new Line(Convert.ToInt32(x), Convert.ToInt32(y), Convert.ToInt32(dlina));
						figure.FactoryPaint(paint_box, figure, pen);
						return figure;
					case "Сircle":
						figure = new Сircle(Convert.ToInt32(x), Convert.ToInt32(y), Convert.ToInt32(dlina));
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
				string save1 = "<>\n";
				for (int i = 0; i < count; i++)
				{
					save1 += group[i].save() + "\n";
				}
				save1 += "</>";
				return save1;
			}
			private int maxcount = 10;
			public int count;
			public FigureAbstract[] group;
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

					notifyEveryone(group[i]);
				}
			}
			public override void moveY(int k)
			{
				for (int i = 0; i < count; i++)
				{
					group[i].moveY(k);
					notifyEveryone(group[i]);
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
				x += k;
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
		public class Line : FigureIF
		{
			public override string save()
			{
				string objec = "";
				objec += "Line\n" + getX() + "\n" + getY() + "\n" + getsize();
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
			public override void FactoryPaint(Panel paint_box, FigureAbstract figure, Pen pen)
			{
				pen.Color = figure.getColor();//перекрываю метод, фабричного метода
				pen.Width = 5;
				paint_box.CreateGraphics().DrawLine(pen, figure.getX() - getsize(),
					figure.getY(), figure.getX() + getsize(), figure.getY());
				pen.Width = 3;
			}
			public override void changeSize(int k)
			{
				LineLength += k;
			}
			public override void moveX(int k)
			{
				if (getX() - LineLength > 0)
				{
					this.setX(k);
				}
				else
					this.setX(Math.Abs(k));

				notifyEveryone(this);
			}
			public override void moveY(int k)
			{
				if (getY() > 0)
				{
					this.setY(k);
				}
				else
					this.setY(Math.Abs(k));

				notifyEveryone(this);
			}
			public override bool checkFigure(FigureAbstract figure, MouseEventArgs e, Panel paint_box, Pen pen)
			{
				if (e.X >= figure.getX() - getsize()
					&& e.X <= figure.getX() + getsize()
					&& e.Y >= figure.getY() - 2
					&& e.Y <= figure.getY() + 2)
				{
					return true;
				}
				else return false;
			}
		}
		public class Сircle : FigureIF
		{
			public override string save()
			{
				string objec = "";
				objec += "Сircle\n" + getX() + "\n" + getY() + "\n" + getsize();
				return objec;
			}
			private int СircleRad = 35; //радиус круга
			public override int getsize() { return СircleRad; }
			public Сircle(int x, int y) : base(x, y)//конструктор с параметрами
			{

			}
			public Сircle(int x, int y, int dlina) : base(x, y)//конструктор с параметрами
			{
				СircleRad = dlina;
			}
			public override void FactoryPaint(Panel paint_box, FigureAbstract figure, Pen pen)
			{
				pen.Color = figure.getColor();//перекрываю метод фабричный
				paint_box.CreateGraphics().DrawEllipse(pen, figure.getX() - getsize(),
						figure.getY() - getsize(), getsize() * 2, getsize() * 2);
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
				notifyEveryone(this);
			}
			public override void moveY(int k)
			{
				if (getY() - СircleRad > 0)
				{
					this.setY(k);
				}
				else
					this.setY(Math.Abs(k));
				notifyEveryone(this);
			}
			public override bool checkFigure(FigureAbstract figure, MouseEventArgs e, Panel paint_box, Pen pen)
			{
				if ((figure.getX() - e.X) * (figure.getX() - e.X) +
					(figure.getY() - e.Y) * (figure.getY() - e.Y)
					<= getsize() * getsize())
				{
					return true;
				}
				else return false;
			}
		}
		public class Observer
		{
			public virtual void onSubjectChanged(mystorage who) { }
			public virtual void onSubjectChanged(FigureAbstract obj) { }
		}
		public class StickyObserver : Observer
		{
			mystorage storageCopy;
            public StickyObserver(mystorage stor)
            {
				storageCopy = (mystorage)stor.Clone();
			}
			public bool checkLine(FigureAbstract obj, FigureAbstract storageObj)
            {
				if(obj.getX() + obj.getsize() >= storageObj.getX() - storageObj.getsize()
					&& obj.getX() - obj.getsize() <= storageObj.getX() + storageObj.getsize()
					&& obj.getY() >= storageObj.getY() - 2
					&& obj.getY() <= storageObj.getY() + 2)
					return true;
				else
					return false;
			}
			public bool checkCircle(FigureAbstract obj, FigureAbstract storageObj)
			{
				int N = (storageObj.getX() - obj.getX()) * (storageObj.getX() - obj.getX()) +
					(storageObj.getY() - obj.getY()) * (storageObj.getY() - obj.getY());
				if ((storageObj.getX() - obj.getX()) * (storageObj.getX() - obj.getX()) +
					(storageObj.getY() - obj.getY()) * (storageObj.getY() - obj.getY())
					<= (obj.getsize() + storageObj.getsize()) *( obj.getsize() + storageObj.getsize())+1)
					return true;
				else 
					return false;
			}
			public bool FigureCheck(FigureAbstract obj, FigureAbstract storageObj, string name)
            {
                switch (name)
                {
					case "Сircle":
						if (checkCircle(obj, storageObj))
							return true;
						break;

					case "Line":
						if (checkLine(obj, storageObj))
							return true;
						break;

					case "<>":
						for (int i = 0; i < (storageObj as GroupFigures).count; i++)
						{
							string name1 = (storageObj as GroupFigures).group[i].save();
							name1 = name1.Remove(name1.IndexOf('\n'), name1.Length - name1.IndexOf('\n'));

							if (FigureCheck(obj, (storageObj as GroupFigures).group[i], name1))
							{
								storageCopy.getobj().setColor(Color.Orange);
								return true;
							}
							else 
								return false;
						}
						break;
				}
				return false;
            }
			public override void onSubjectChanged(FigureAbstract obj)
			{
				for (storageCopy.First(); storageCopy.EOL(); storageCopy.Next())//нашли next = obj 
				{
					if (storageCopy.getobj() is GroupFigures)
					{
						for (int i = 0; i < (storageCopy.getobj() as GroupFigures).count; i++)
						{
							string name1 = (storageCopy.getobj() as GroupFigures).group[i].save();
							name1 = name1.Remove(name1.IndexOf('\n'), name1.Length - name1.IndexOf('\n'));

							if (FigureCheck(obj, (storageCopy.getobj() as GroupFigures).group[i], name1))
								storageCopy.getobj().setColor(Color.Orange);
						}
					}
					else
					{
						string name1 = storageCopy.getobj().save();
						name1 = name1.Remove(name1.IndexOf('\n'), name1.Length - name1.IndexOf('\n'));
						if (FigureCheck(obj, storageCopy.getobj(),name1))
							storageCopy.getobj().setColor(Color.Orange);
					}
					
				}
			}

		}
		public class TreeViewer : Observer
		{
			TreeView tree;
			public delegate void func(TreeView tree, TreeNode tn);
			func selectOBj;
			public TreeViewer(Form form, func temp)
			{
				selectOBj = temp;
				tree = new TreeView();
				tree.Location = new System.Drawing.Point(721, 298);
				tree.Size = new System.Drawing.Size(208, 313);
				tree.Font = new Font("Microsoft Sans Serif", 14);
				tree.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Bottom)));
				tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tree_AfterSelect);
				form.Controls.Add(tree);
			}
			public void tree_AfterSelect(object sender, TreeViewEventArgs e)//Выделение через дерево
			{
                if (e.Node.Parent != null && e.Action == TreeViewAction.ByMouse)
                {
                    selectOBj(tree, e.Node);
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
		public void selectOBj(TreeView tree, TreeNode tn)
		{
			if (tn.Parent.Text != "Фигуры")
			{
				selectOBj(tree, tn.Parent);
			}
			else
			{
				int index = tn.Index, check = 0;
				for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
				{
					if (check == index)
					{
						printFigure(Storage.getobj(), Color.Orange);
						check++;
						continue;
					}
					printFigure(Storage.getobj(), Color.Red);
					check++;
				}
				return;
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
				N -= delcount;

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
			if (clickCheck(e, newtree)) //проверка 
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
				paint_box.Refresh();
				for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
				{
					if (Storage.getobj().getColor() == Color.Orange && e.KeyCode.Equals(Keys.A))
						Storage.getobj().moveX(-1); //если влево
					else if (Storage.getobj().getColor() == Color.Orange)
						Storage.getobj().moveX(1); //если вправо
					Storage.getobj().FactoryPaint(paint_box, Storage.getobj(), pen);
				}
			}
			else if (e.KeyCode.Equals(Keys.W) || e.KeyCode.Equals(Keys.S))
			{ //если нажал вверх или вниз
				paint_box.Refresh();
				for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
				{
					if (Storage.getobj().getColor() == Color.Orange && e.KeyCode.Equals(Keys.W))
						Storage.getobj().moveY(-1); //если вверх
					else if (Storage.getobj().getColor() == Color.Orange)
						Storage.getobj().moveY(1); //если вниз
					Storage.getobj().FactoryPaint(paint_box, Storage.getobj(), pen);
				}
			}
			else if (e.KeyCode.Equals(Keys.Add) || e.KeyCode.Equals(Keys.Subtract))
			{ //если нажал + или -
				paint_box.Refresh();
				for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
				{
					if (Storage.getobj().getColor() == Color.Orange && e.KeyCode.Equals(Keys.Add))
						Storage.getobj().changeSize(k);
					else if (Storage.getobj().getColor() == Color.Orange)
						Storage.getobj().changeSize(-k);
					Storage.getobj().FactoryPaint(paint_box, Storage.getobj(), pen);
				}
			}
			else if (e.KeyCode.Equals(Keys.Delete))
			{ //если нажал delete
				paint_box.Refresh();
				for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
				{
					if (Storage.getobj().getColor() == Color.Orange)
					{
						Storage.delobj(Storage.getobj());//удаляю объект выделенный
						continue;
					}
					Storage.getobj().FactoryPaint(paint_box, Storage.getobj(), pen);
				}
			}
		}
		private void textBox3_Click(object sender, EventArgs e)
		{
			paint_box.Refresh();
			for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
			{
				if (Storage.getobj().getColor() == Color.Orange)
				{
					Storage.getobj().setColor(((TextBox)sender).BackColor);
				} //изменение цвета
				Storage.getobj().FactoryPaint(paint_box, Storage.getobj(), pen);
			}
		}
		private void groupButton_Click(object sender, EventArgs e)
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
			Storage = new mystorage();
			Storage.addObserver(newtree);
		}
		string path = @"C:\OOP_LABA7\filename.txt";
		private void input_Click(object sender, EventArgs e)
		{
			Storage = new mystorage();
			Storage.addObserver(newtree);
			paint_box.Refresh();
			String smth, code = "", x = "", y = "", dlina = ""; int countFigures = 0;
			using (StreamReader sr = new StreamReader(path))
			{
				factory fact = new factory();
				countFigures = Convert.ToInt32(sr.ReadLine());
				countFigures = countFigures * 4;
				while (countFigures > 0)
				{
					smth = sr.ReadLine();
					if (smth == "<>")
					{
						Stack<string> stack = new Stack<string>();
						Stack<string> stkFinal = new Stack<string>();
						List<FigureAbstract> list = new List<FigureAbstract>();
						stack.Push("<>");
						while (stack.Count != 0)
						{
							smth = sr.ReadLine();
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
						Storage.add(fact.FactoryPaintNEW(paint_box, pen, smth, sr.ReadLine()
							, sr.ReadLine(), sr.ReadLine()));
						countFigures -= 4;
					}

				}
			}
		}
        private void unGroupButton_Click(object sender, EventArgs e)
        {
			int n = Storage.getN();
			for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
			{
				if (Storage.getobj().getColor() == Color.Orange && Storage.getobj() is GroupFigures)
				{
					for(int i= (Storage.getobj() as GroupFigures).count-1; i>=0; i--)
                    {
						Storage.adddown((Storage.getobj() as GroupFigures).group[i], Storage.getobj());
					}
					Storage.delobj(Storage.getobj());
					Storage.setN(n);
					return;
				}
			}
			Storage.setN(n);
		}

        private void button1_Click(object sender, EventArgs e)
		{
			StickyObserver observ = new StickyObserver(Storage);
			for (Storage.First(); Storage.EOL(); Storage.Next())//нашли next = obj 
			{
				if (Storage.getobj().getColor() == Color.Orange)
				{
					Storage.getobj().addObserver(observ);
				}
			}
		}
    }
}
