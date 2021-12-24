using Microsoft.Win32;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Windows;
using System.Windows.Media.Imaging;

namespace AutoPartSystem.ViewModel.Warehouse
{
    public class ImageGood : Data.GoodsImage
    {
        private BitmapImage _image;

        public BitmapImage Image
        {
            get => _image;
            set => this.RaiseAndSetIfChanged(ref _image, value);

        }
        public void UpdatePhoto()
        {
            this.Image = ToImage(this.ImageByte);
        }
        private BitmapImage ToImage(byte[] array)//Делаем из потока байтов картинку
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                try
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.CacheOption = BitmapCacheOption.OnLoad; // here
                    image.StreamSource = ms;
                    image.EndInit();
                    return image;
                }
                catch
                {
                    return null;
                }
            }
        }
        public ImageGood(Data.GoodsImage goodsImage)
        {
            this.Id = goodsImage.Id;
            this.ImageByte = goodsImage.ImageByte;
            this.Name = goodsImage.Name;
            this.GoodId = goodsImage.GoodId;
            UpdatePhoto();
        }
        public ImageGood() { }
    }
    public class GoodCardViewModel : ReactiveObject
    {
        private Model.Warehouse.WarehouseModel WarehouseModel;
        private Model.MarkModel.MarkModel MarkModel;
        private Data.Warehouse _warehouse;
        public Data.Warehouse Warehouse
        {
            get => _warehouse;
            set => this.RaiseAndSetIfChanged(ref _warehouse, value);
        }
        private Data.Model _model;
        public Data.Model Model
        {
            get => _model;
            set => this.RaiseAndSetIfChanged(ref _model, value);
        }
        private ObservableCollection<Data.Model>? _models;
        public ObservableCollection<Data.Model> Models
        {
            get => _models;
            set => this.RaiseAndSetIfChanged(ref _models, value);
        }
        private ObservableCollection<Data.Model> _models1;
        public ObservableCollection<Data.Model> Models1
        {
            get => _models1;
            set => this.RaiseAndSetIfChanged(ref _models1, value);
        }
        private ObservableCollection<Data.Mark> _marklist;
        public ObservableCollection<Data.Mark> MarkList
        {
            get => _marklist;
            set => this.RaiseAndSetIfChanged(ref _marklist, value);
        }
        private ObservableCollection<Data.Brand> _brands;
        public ObservableCollection<Data.Brand> Brands
        {
            get => _brands;
            set => this.RaiseAndSetIfChanged(ref _brands, value);
        }
        private string _brand_name;
        public string BrandName
        {
            get => _brand_name;
            set => this.RaiseAndSetIfChanged(ref _brand_name, value);
        }
        private string _mark_name;
        public string MarkName
        {
            get => _mark_name;
            set => this.RaiseAndSetIfChanged(ref _mark_name, value);
        }
        private string _model_name;
        public string ModelName
        {
            get => _model_name;
            set => this.RaiseAndSetIfChanged(ref _model_name, value);
        }
        private Data.Mark _mark;
        public Data.Mark Mark
        {
            get => _mark;
            set => this.RaiseAndSetIfChanged(ref _mark, value);
        }
        ////////////////////////////////////
        private Data.Model _model1;
        public Data.Model Model1
        {
            get => _model1;
            set => this.RaiseAndSetIfChanged(ref _model1, value);
        }
        private string _mark_name1;
        public string MarkName1
        {
            get => _mark_name1;
            set => this.RaiseAndSetIfChanged(ref _mark_name1, value);
        }
        private string _model_name1;
        public string ModelName1
        {
            get => _model_name1;
            set => this.RaiseAndSetIfChanged(ref _model_name1, value);
        }
        private Data.Mark _mark1;
        public Data.Mark Mark1
        {
            get => _mark1;
            set => this.RaiseAndSetIfChanged(ref _mark1, value);
        }
        private ObservableCollection<Data.GoodsModel> _goods_model;
        public ObservableCollection<Data.GoodsModel> GoodsModel
        {
            get => _goods_model;
            set => this.RaiseAndSetIfChanged(ref _goods_model, value);
        }
        private ObservableCollection<Data.GoodsModel> GoodsModelRemove;
        private ImageGood _goods_image;
        public ImageGood GoodsImage
        {
            get => _goods_image;
            set => this.RaiseAndSetIfChanged(ref _goods_image, value);
        }
        private WarehouseTable _warehouse_stock;
        public int PosId { get; private set; }
        public GoodCardViewModel(Model.Warehouse.WarehouseModel WarehouseModel, Model.MarkModel.MarkModel MarkModel, Data.Warehouse Warehouse)
        {
            Back = ReactiveCommand.Create(() => { Console.Write("123"); MainViewModel.WarehouseViewModel.OpenPageCommand("ZavSkladTable"); });
            PosId = MainViewModel.PositId;
            _warehouse_stock = WarehouseTable.NewTable(Warehouse as WarehouseTable);
            this.Warehouse = Warehouse;
            Console.WriteLine(Warehouse.Goods.GoodsModel.Count);
            var model = Warehouse.Goods.GoodsModel.FirstOrDefault();
            GoodsModel = new ObservableCollection<Data.GoodsModel>(Warehouse.Goods.GoodsModel.ToList());
            GoodsModel.Remove(model);
            if (model != null)
            {
                ModelName = model.Model.Name;
                MarkName = model.Model.Mark.Name;
                Model = new Data.Model(model.Model.Id, model.Model.Name, model.Model.MarkId, model.Model.Mark);

                Mark = model.Model.Mark;
                Models = MarkModel.GetModelFromMarkId(Mark.Id);
            }
            else
            {
                Model = new Data.Model(); ModelName = ""; MarkName = "";
                Mark = new Data.Mark();
            }

            Mark1 = new Data.Mark();

            BrandName = Warehouse.Goods.Brand.Name;

            MarkList = MarkModel.GetMark();
            Brands = MarkModel.GetBrand();

            this.WarehouseModel = WarehouseModel;
            this.MarkModel = MarkModel;
            GoodsImage = WarehouseModel.GetGoodImage(Warehouse.Goods.Id);

        }
        public GoodCardViewModel(Model.Warehouse.WarehouseModel WarehouseModel, Model.MarkModel.MarkModel MarkModel, Data.Warehouse Warehouse,bool isvirtual)
        {
            Back = ReactiveCommand.Create(() => { MainViewModel.WarehouseViewModel.OpenPageCommand("VirtualSkladTable"); });
            PosId = MainViewModel.PositId;
            _warehouse_stock = WarehouseTable.NewTable(Warehouse as WarehouseTable);
            this.Warehouse = Warehouse;
            Console.WriteLine(Warehouse.Goods.GoodsModel.Count);
            var model = Warehouse.Goods.GoodsModel.FirstOrDefault();
            GoodsModel = new ObservableCollection<Data.GoodsModel>(Warehouse.Goods.GoodsModel.ToList());
            GoodsModel.Remove(model);
            if (model != null)
            {
                ModelName = model.Model.Name;
                MarkName = model.Model.Mark.Name;
                Model = new Data.Model(model.Model.Id, model.Model.Name, model.Model.MarkId, model.Model.Mark);

                Mark = model.Model.Mark;
                Models = MarkModel.GetModelFromMarkId(Mark.Id);
            }
            else
            {
                Model = new Data.Model(); ModelName = ""; MarkName = "";
                Mark = new Data.Mark();
            }

            Mark1 = new Data.Mark();

            MarkList = MarkModel.GetMark();
            Brands = MarkModel.GetBrand();

            this.WarehouseModel = WarehouseModel;
            this.MarkModel = MarkModel;
            GoodsImage = WarehouseModel.GetGoodImage(Warehouse.Goods.Id);

        }
        public ReactiveCommand<int, Unit> SelectModelFromMark => ReactiveCommand.Create<int>(SelectModelFromMarkCommand);
        private void SelectModelFromMarkCommand(int mark_id)
        {

            Models = MarkModel.GetModelFromMarkId(mark_id);
            Model = new Data.Model();
            ModelName = "";
        }
        public ReactiveCommand<int, Unit> SelectModelFromMark1 => ReactiveCommand.Create<int>(SelectModelFromMarkCommand1);
        private void SelectModelFromMarkCommand1(int mark_id)
        {
            if (Mark1 == null) return;
            Console.WriteLine(mark_id + " " + Mark1.Id);
            Models1 = MarkModel.GetModelFromMarkId(mark_id);
            Console.WriteLine(Models1.Count);
            Model1 = new Data.Model();
            ModelName1 = "";
        }
        public ReactiveCommand<Unit, Unit> SaveWarehouse => ReactiveCommand.Create(() =>
        {
            var model = new Data.Model();
            var mark = new Data.Mark();
            if (Model != null)
            {
                model = new Data.Model(Model.Id, Model.Name, Mark.Id, Mark);
            }
            else
            {


                if (Mark != null)
                {
                    if (Model != null)
                    {
                        model = MarkModel.GetModelFromNameFind(ModelName, Mark.Id);
                        if (model == null)
                        {
                            model = new Data.Model(ModelName, Mark.Id);
                        }
                        else
                        {
                            model.Mark = null;
                        }
                    }
                    else
                    {
                        model = new Data.Model(ModelName, Mark.Id);
                    }
                }
                else
                {
                    mark = MarkModel.GetMarkFromNameFind(MarkName);
                    if (mark == null)
                    {
                        model = new Data.Model(ModelName, new Data.Mark(MarkName));
                    }
                    else
                    {
                        if (Model == null)
                        {
                            model = MarkModel.GetModelFromNameFind(ModelName, mark.Id);
                            if (model == null)
                            {
                                model = new Data.Model(ModelName, mark.Id);
                            }
                            else
                            {
                                model.Mark = null;
                            }
                        }
                        else
                        {
                            model = new Data.Model(ModelName, mark.Id);
                        }
                    }
                }
            }
            if (model.Id == 0)
            {
                MarkModel.AddModel(model);


            }
            var good_model = Warehouse.Goods.GoodsModel.FirstOrDefault();
            if (good_model != null)
            {
                good_model.ModelId = model.Id;

            }
            else
            {
                good_model = new Data.GoodsModel { GoodsId = Warehouse.Goods.Id, ModelId = model.Id };
            }
            WarehouseModel.UpdateGoodMode(good_model);
            using var db = new Data.ConDB();
            if (GoodsModelRemove != null)
                db.RemoveRange(GoodsModelRemove);

            WarehouseModel.UpdateGoodModel(GoodsModel, GoodsModelRemove);
            GoodsModelRemove = new ObservableCollection<Data.GoodsModel>();
            Warehouse.Goods.GoodsModel = null;
            WarehouseModel.UpdateWarehouse(Warehouse);
            WarehouseModel.UpdateAll();
            Back.Subscribe();
        });
        public ReactiveCommand<Unit, Unit> AddModelToWareCom => ReactiveCommand.Create(() =>
        {
            var model = new Data.Model();
            var mark = new Data.Mark();
            if (MarkName1 == "" || ModelName1 == "") return;
            if (MarkName.ToLower() == MarkName1.ToLower() && ModelName1.ToLower() == ModelName.ToLower()) { MessageBox.Show("Данная модель уже добавлена ", " Ошибка"); return; }
            if (GoodsModel.Any(p => p.Model.Name.ToLower() == ModelName1.ToLower() && p.Model.Mark.Name.ToLower() == MarkName1.ToLower()))
            {
                MessageBox.Show("Данная модель уже добавлена ", " Ошибка"); return;
            }
            if (Mark1 != null)
            {
                if (Model1 != null)
                {
                    Model1.Mark = new Data.Mark(Mark1.Id, Mark1.Name);
                    GoodsModel.Add(new Data.GoodsModel { Model = new Data.Model(Model1.Id, Model1.Name, Model1.MarkId, Model1.Mark), ModelId = Model1.Id, GoodsId = Warehouse.Goods.Id });
                }
                else
                {
                    model = MarkModel.GetModelFromNameFind(ModelName1, Mark1.Id);
                    if (model != null)
                    {
                        model.Mark = new Data.Mark(Mark1.Id, Mark1.Name);
                        GoodsModel.Add(new Data.GoodsModel { Model = model, ModelId = model.Id, GoodsId = Warehouse.Goods.Id });
                    }
                    else
                    {
                        GoodsModel.Add(new Data.GoodsModel { Model = new Data.Model(ModelName1, Mark1.Id, new Data.Mark(Mark1.Id, Mark1.Name)), GoodsId = Warehouse.Goods.Id });
                    }
                }
            }
            else
            {
                mark = MarkModel.GetMarkFromNameFind(MarkName1);
                if (mark != null)
                {
                    model = MarkModel.GetModelFromNameFind(ModelName1, mark.Id);
                    if (model != null)
                    {
                        model.Mark = new Data.Mark(mark.Id, mark.Name);
                        GoodsModel.Add(new Data.GoodsModel { Model = model, ModelId = model.Id, GoodsId = Warehouse.Goods.Id });
                    }
                    else
                    {
                        GoodsModel.Add(new Data.GoodsModel { Model = new Data.Model(ModelName1, mark.Id, mark), GoodsId = Warehouse.Goods.Id });
                    }
                }
                else
                {
                    GoodsModel.Add(new Data.GoodsModel { Model = new Data.Model(ModelName1, new Data.Mark(MarkName1)), GoodsId = Warehouse.Goods.Id });
                }
            }
            Model1 = new Data.Model();
            Mark1 = new Data.Mark();
            ModelName1 = "";
            MarkName1 = "";
            Models1 = new ObservableCollection<Data.Model>();


        });
        public ReactiveCommand<Data.GoodsModel, Unit> DelGoodModel => ReactiveCommand.Create<Data.GoodsModel>(DelGoodModelCommand);
        public ReactiveCommand<Unit, Unit> Back { get; set; }
        public ReactiveCommand<Unit, Unit> DeleteWarehouse => ReactiveCommand.Create(() => { Warehouse.IsDelete = true; WarehouseModel.UpdateWarehouse(Warehouse); WarehouseModel.UpdateAll(); Back.Subscribe(); });
        private void DelGoodModelCommand(Data.GoodsModel goodsModel)
        {
            GoodsModel.Remove(goodsModel);
            if (goodsModel.Id != 0)
            {
                if (GoodsModelRemove == null) GoodsModelRemove = new ObservableCollection<Data.GoodsModel>();

                GoodsModelRemove.Add(goodsModel);
            }
        }
        public ReactiveCommand<Unit, Unit> SelectGoodPicture => ReactiveCommand.Create(() => {

            OpenFileDialog myDialog = new OpenFileDialog();
            myDialog.Filter = "Image files(*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
            myDialog.CheckFileExists = true;
            if (GoodsImage == null)
            {
                GoodsImage = new ImageGood();
            }
            if (myDialog.ShowDialog() == true)
            {

                GoodsImage.ImageByte = File.ReadAllBytes(myDialog.FileName);
                GoodsImage.UpdatePhoto();
                GoodsImage.Name = Path.GetFileName(myDialog.FileName);
                GoodsImage.GoodId = Warehouse.Goods.Id;
                WarehouseModel.UpdateGoodImage(GoodsImage);
            }

        });
    }
}
