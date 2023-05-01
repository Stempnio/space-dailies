using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SpaceDailies;

public abstract class BaseViewModel : ObservableObject
{
    public BaseViewModel()
    {
        init();
    }

    abstract public void init();
}

