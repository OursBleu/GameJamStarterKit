using UnityEngine;

static class Grid
{
    public static GlobalManagerFrameAnimation animationSystem;
    public static GlobalManagerParticles particleSystem;

    // when the program launches, Grid will check that all the needed elements are in place
    // that's exactly what you do in the static constructor here:
    static Grid()
    {
        GameObject g;

        g = safeFind("__GlobalManagers"); // (some persistent game object)

        animationSystem = (GlobalManagerFrameAnimation)safeComponent(g, "GlobalManagerFrameAnimation");
        particleSystem = (GlobalManagerParticles)safeComponent(g, "GlobalManagerParticles");
    }

    // this has no purpose other than for developers wondering HTF you use Grid
    // just type Grid.SayHello() anywhere in the project.
    // it is useful to add a similar routine to (example) PurchaseManager.cs
    // then from anywhere in the project, you can type Grid.purchaseManager.SayHello()
    // to check everything is hooked-up properly.
    public static void SayHello()
    {
        Debug.Log("Confirming to developer that the Grid is working fine.");
    }

    private static GameObject safeFind(string s)
    {
        GameObject g = GameObject.Find(s);
        if (g == null) bigProblem("The " + s + " game object is not in this scene. You're stuffed.");
        // next .... see Vexe to check that there is strictly ONE of these fuckers. you never know.
        return g;
    }
    private static Component safeComponent(GameObject g, string s)
    {
        Component c = g.GetComponent(s);
        if (c == null) bigProblem("The " + s + " component is not there. You're stuffed.");
        return c;
    }
    private static void bigProblem(string error)
    {
        for (int i = 10; i > 0; --i) Debug.LogError(" >>> Cannot proceed... " + error);
        for (int i = 10; i > 0; --i) Debug.LogError(" !!!!!  Is it possible you just forgot to launch from scene zero.");
        Debug.Break();
    }
}