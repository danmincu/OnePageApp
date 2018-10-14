using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Mvvm;

namespace OnePageApp.Services
{
    public abstract class BasePermission : BindableBase
    {
        public Action<bool> OnSelectionChanged { get; set; }

        private bool isSelected;

        public bool IsSelected
        {
            get => isSelected;
            set
            {
                SetProperty(ref isSelected, value);
                if (this.Items != null)
                {
                    foreach (var baseOrganization in Items)
                    {
                        baseOrganization.IsSelected = value;
                    }
                }
                //notify parents
                this.OnSelectionChanged?.Invoke(value);
            }
        }

        public void ChildSelectionChanged(bool value)
        {
            var new_value = !value ? value : (this.Items != null && this.Items.All(itm => itm.IsSelected));
            SetProperty(ref isSelected, new_value);
            RaisePropertyChanged(nameof(BasePermission.IsSelected));
            this.OnSelectionChanged?.Invoke(new_value);
        }

        public abstract int Level { get; }
        public int Code { get; set; }
        public string Name { get; set; }

        public BasePermission Parent { set; get; }

        public List<BasePermission> PermissionPath()
        {
            var result = new List<BasePermission>();
            this.ComputePermissionCodePath(result, this);
            result.Reverse();
            return result;
        }

        private void ComputePermissionCodePath(List<BasePermission> list, BasePermission basePermission)
        {
            list.Add(basePermission);
            if (basePermission.Parent != null)
                this.ComputePermissionCodePath(list, basePermission.Parent);
        }

        private IEnumerable<BasePermission> items;

        public IEnumerable<BasePermission> Items
        {
            get => items;
            set
            {
                SetProperty(ref items, value);
                if (items != null)
                {
                    foreach (var baseOrganization in items)
                    {
                        baseOrganization.Parent = this;
                        baseOrganization.OnSelectionChanged += ChildSelectionChanged;
                    }
                }
            }
        }


        public override bool Equals(object obj)
        {
            if (!(obj is BasePermission))
                return false;

            var other = obj as BasePermission;

            if (Name != other.Name || Code != other.Code || Level != other.Level)
                return false;

            return true;
        }


        public static bool operator ==(BasePermission x, BasePermission y)
        {
            if (ReferenceEquals(x, null) && ReferenceEquals(y, null))
                return true;
            if (ReferenceEquals(x, null)) return false;
            return x.Equals(y);
        }

        public static bool operator !=(BasePermission x, BasePermission y)
        {
            return !(x == y);
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

    }
}