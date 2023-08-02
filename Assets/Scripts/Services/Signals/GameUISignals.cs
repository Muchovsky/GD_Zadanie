using Zenject;

public class GameUISignals
{
    public static void DeclareSignals(DiContainer container)
    {
        container.DeclareSignal<TabChanged>();
    }

    public class TabChanged
    {
        public int CurrentTab;
    }
}
