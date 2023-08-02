using Zenject;

public class ButtonClickedSignal
{
    public static void DeclareSignals(DiContainer container)
    {
        container.DeclareSignal<PreviousButtonClicked>();
        container.DeclareSignal<NextButtonClicked>();
    }

    public class PreviousButtonClicked
    {
    }

    public class NextButtonClicked
    {
    }
}
