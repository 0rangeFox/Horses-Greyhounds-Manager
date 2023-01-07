namespace HaGManager.Extensions; 

public static class StackExtension {

    public static void Add<T>(this Stack<T> stack, T value) {
        stack.Push(value);
    }

}
