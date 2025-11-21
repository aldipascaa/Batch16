using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DisposalAndGarbageCollection;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("What do you want to learn?");
        int.TryParse(Console.ReadLine(), out int x);
        do
        {
            switch (x)
            {
                case 1:
                    Console.WriteLine("---------");
                    Console.WriteLine("CHAPTER 1");
                    Console.WriteLine("IDisposable, Dispose, and Close");
                    IDisposableAndCloseDemo();
                    break;
                case 2:
                    Console.WriteLine("---------");
                    Console.WriteLine("CHAPTER 2");
                    Console.WriteLine("Automatic Garbage Collection: Memory Management in .NET");
                    AutomaticGarbageCollectionDemo();
                    break;
                case 3:
                    Console.WriteLine("---------");
                    Console.WriteLine("CHAPTER 3");
                    Console.WriteLine("Finalizers: Last-Resort Cleanup");
                    FinalizersDemo([]);
                    break;
                case 4:
                    Console.WriteLine("---------");
                    Console.WriteLine("CHAPTER 4");
                    Console.WriteLine("How the Garbage Collector Works");
                    HowGCWorksDemo();
                    break;
                case 5:
                    Console.WriteLine("---------");
                    Console.WriteLine("CHAPTER 5");
                    Console.WriteLine("Managed Memory Leaks: Unintended Object Retention");
                    ManagedMemoryLeaksDemo();
                    break;
                case 6:
                    Console.WriteLine("---------");
                    Console.WriteLine("CHAPTER 6");
                    Console.WriteLine("Weak References: Observing Without Rooting");
                    WeakReferenceDemo();
                    break;
                case 7:
                    Console.WriteLine("---------");
                    Console.WriteLine("CHAPTER 7");
                    Console.WriteLine("Conditional Compilation");
                    ConditionalCompilationDemo();
                    break;
                case 8:
                    Console.WriteLine("---------");
                    Console.WriteLine("CHAPTER 8");
                    Console.WriteLine("Debug and Trace Classes: Essential Diagnostic Tools");
                    DebugAndTraceClassesDemo();
                    break;
            }
            Console.WriteLine("Pick a number that you want to learn!");
            int.TryParse(Console.ReadLine(), out x);
        }
        while(x>0);
    }
    static void IDisposableAndCloseDemo()
    {
                    Console.WriteLine("=== IDisposable Master Class: From Basics to Advanced Patterns ===\n");

            // Let's start with the fundamentals and work our way up
            Console.WriteLine("📚 LESSON 1: The 'using' Statement - Your Safety Net");
            DemonstrateUsingStatement();
            Console.WriteLine();

            Console.WriteLine("📚 LESSON 2: Standard Disposal Semantics - The Three Golden Rules");
            DemonstrateDisposalSemantics();
            Console.WriteLine();

            Console.WriteLine("📚 LESSON 3: Close() vs Dispose() - Know the Difference");
            DemonstrateCloseVsDispose();
            Console.WriteLine();

            Console.WriteLine("📚 LESSON 4: Chained Disposal - When Objects Own Other Objects");
            DemonstrateChainedDisposal();
            Console.WriteLine();

            Console.WriteLine("📚 LESSON 5: When NOT to Dispose - Breaking the Rules Safely");
            DemonstrateWhenNotToDispose();
            Console.WriteLine();

            Console.WriteLine("📚 LESSON 6: Clearing Fields in Dispose - Beyond Just Unmanaged Resources");
            DemonstrateFieldClearingInDispose();
            Console.WriteLine();

            Console.WriteLine("📚 LESSON 7: Anonymous Disposal Pattern - IDisposable on the Fly");
            DemonstrateAnonymousDisposal();
            Console.WriteLine();
    }
    static void AutomaticGarbageCollectionDemo()
    {
            Console.WriteLine("=== AUTOMATIC GARBAGE COLLECTION IN .NET ===");
            Console.WriteLine("Comprehensive demonstration of memory management concepts");
            Console.WriteLine("Based on the foundational principles of the .NET CLR");
            Console.WriteLine();
            
            // Fundamental concepts from the training material
            Console.WriteLine("=== PART 1: FUNDAMENTAL CONCEPTS ===");
            
            // Demo 1: The basic Test() method example from training material
            Console.WriteLine("1. Basic Memory Allocation and Object Lifecycle");
            Console.WriteLine("   (The Test() method example from training material)");
            DemoBasicMemoryAllocation();
            Console.WriteLine();
            
            // Demo 2: Indeterminate nature of garbage collection
            Console.WriteLine("2. The Indeterminate Nature of Garbage Collection");
            BasicMemoryExample.DemoIndeterminateCollection();
            Console.WriteLine();
            
            // Demo 3: Debug vs Release behavior
            Console.WriteLine("3. Debug vs Release Object Lifetime");
            BasicMemoryExample.DemoDebugVsReleaseBehavior();
            Console.WriteLine();
            
            // Demo 4: Memory consumption balance
            Console.WriteLine("4. GC Memory Consumption Balance");
            BasicMemoryExample.DemoMemoryConsumptionBalance();
            Console.WriteLine();
            
            // Demo 5: Programmatic memory monitoring
            Console.WriteLine("5. Programmatic Memory Monitoring");
            BasicMemoryExample.DemoMemoryMonitoring();
            Console.WriteLine();
            
            Console.WriteLine("=== PART 2: ROOTS AND REACHABILITY ===");
            
            // Demo 6: Comprehensive roots demonstration
            Console.WriteLine("6. All Types of Roots (Complete Coverage)");
            RootsAndReachabilityDemo.DemoAllRootTypes();
            Console.WriteLine();
            
            // Demo 7: Reachability scenarios
            Console.WriteLine("7. Reachability Scenarios");
            RootsAndReachabilityDemo.DemoReachabilityScenarios();
            Console.WriteLine();
            
            // Demo 8: Instance method reachability
            Console.WriteLine("8. Instance Method Reachability");
            RootsAndReachabilityDemo.DemoInstanceMethodReachability();
            Console.WriteLine();
            
            Console.WriteLine("=== PART 3: GENERATIONAL GARBAGE COLLECTION ===");
            
            // Demo 9: Complete generational system
            Console.WriteLine("9. Generational Garbage Collection System");
            GenerationalGCDemo.DemoGenerationalSystem();
            Console.WriteLine();
            
            // Demo 10: Indeterminate delay
            Console.WriteLine("10. Indeterminate Collection Delay");
            GenerationalGCDemo.DemoIndeterminateDelay();
            Console.WriteLine();
            
            Console.WriteLine("=== PART 4: ADVANCED CONCEPTS ===");
            
            // Demo 11: Circular references (from training material)
            Console.WriteLine("11. Circular References and GC");
            DemoCircularReferences();
            Console.WriteLine();
            
            // Demo 12: Large Object Heap
            Console.WriteLine("12. Large Object Heap (LOH) Behavior");
            DemoLargeObjectHeap();
            Console.WriteLine();
            
            // Demo 13: Memory pressure and triggers
            Console.WriteLine("13. Memory Pressure and Collection Triggers");
            DemoMemoryPressure();
            Console.WriteLine();
            
            // Demo 14: Weak references
            Console.WriteLine("14. Weak References and GC");
            DemoWeakReferences();
            Console.WriteLine();
            
            // Demo 15: Finalization and disposal
            Console.WriteLine("15. Finalization and Disposal Patterns");
            DemoFinalizationAndDisposal();
            Console.WriteLine();
            
            Console.WriteLine("=== DEMONSTRATION COMPLETE ===");
            Console.WriteLine();
            Console.WriteLine("KEY TAKEAWAYS FROM THIS DEMONSTRATION:");
            Console.WriteLine("• Memory management in .NET is fully automatic");
            Console.WriteLine("• Objects are allocated on the heap, references on the stack");
            Console.WriteLine("• Garbage collection is indeterminate - eligibility ≠ immediate collection");
            Console.WriteLine("• The GC traces from roots to determine reachability");
            Console.WriteLine("• Generational collection optimizes for object lifetime patterns");
            Console.WriteLine("• Circular references don't prevent collection in .NET");
            Console.WriteLine("• The GC self-tunes based on application behavior");
            Console.WriteLine("• Understanding these concepts helps write memory-efficient code");
            Console.WriteLine();
            Console.WriteLine("IMPORTANT: Never call GC.Collect() in production code!");
            Console.WriteLine("The GC is smarter than we are and knows when to run optimally.");
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");

    }
    static void FinalizersDemo(string[] args)
    {
            Console.WriteLine("=== C# Finalizers: Complete Guide ===\n");

            bool interactive = args.Length == 0 || !args[0].Equals("--non-interactive", StringComparison.OrdinalIgnoreCase);

            // Demo 1: Basic finalizer syntax and behavior
            Console.WriteLine("1. Basic Finalizer Syntax and Behavior:");
            DemoBasicFinalizer();
            if (interactive) WaitForUser();

            // Demo 2: How finalizers work with garbage collection phases
            Console.WriteLine("2. Finalizer Execution Phases:");
            DemoFinalizerPhases();
            if (interactive) WaitForUser();

            // Demo 3: Performance impact and object lifetime extension
            Console.WriteLine("3. Performance Impact and Object Lifetime:");
            DemoPerformanceImpact();
            if (interactive) WaitForUser();

            // Demo 4: Proper dispose pattern implementation
            Console.WriteLine("4. Proper Dispose Pattern with Finalizers:");
            DemoDisposePattern();
            if (interactive) WaitForUser();

            // Demo 5: Object resurrection scenarios
            Console.WriteLine("5. Object Resurrection:");
            DemoObjectResurrection();
            if (interactive) WaitForUser();

            // Demo 6: GC.ReRegisterForFinalize usage
            Console.WriteLine("6. Re-registering for Finalization:");
            DemoReRegisterForFinalize();
            if (interactive) WaitForUser();

            // Demo 7: Common finalizer mistakes and how to avoid them
            Console.WriteLine("7. Common Finalizer Mistakes:");
            DemoCommonMistakes();
            if (interactive) WaitForUser();

            // Demo 8: Finalizer order unpredictability
            Console.WriteLine("8. Finalizer Order Unpredictability:");
            DemoFinalizerOrder();
            if (interactive) WaitForUser();

            Console.WriteLine("=== Demo Complete ===");
            Console.WriteLine("Key takeaway: Use finalizers only when absolutely necessary!");
            Console.WriteLine("Always prefer the Dispose pattern for deterministic cleanup.");
            
            if (interactive)
            {
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nDemo completed in non-interactive mode.");
            }
    }
    static void HowGCWorksDemo(){}
    static void ManagedMemoryLeaksDemo(){}
    static void WeakReferenceDemo(){}
    static void ConditionalCompilationDemo(){}
    static void DebugAndTraceClassesDemo(){}


#region Chapter 1
        /// <summary>
        /// LESSON 1: The 'using' statement is your best friend for disposal.
        /// It's syntactic sugar that the compiler transforms into a try/finally block,
        /// guaranteeing that Dispose() gets called even if an exception occurs.
        /// </summary>
        static void DemonstrateUsingStatement()
        {
            Console.WriteLine("Let's see the 'using' statement in action...\n");

            // First, let's create a temporary file for our demonstration
            string tempFile1 = Path.GetTempFileName();
            string tempFile2 = Path.GetTempFileName();
            File.WriteAllText(tempFile1, "Hello from IDisposable training!");
            File.WriteAllText(tempFile2, "Hello from IDisposable training!");

            // The classic 'using' statement - notice the curly braces
            Console.WriteLine("🔹 Classic 'using' statement:");
            using (var fileManager = new FileManager(tempFile1))
            {
                fileManager.ReadContent();
                Console.WriteLine("Inside the using block - file is open and ready");
            } // <-- Right here, Dispose() is automatically called!
            Console.WriteLine("✅ FileManager disposed automatically when leaving the using block\n");

            // The modern 'using' declaration - cleaner syntax for simple cases
            Console.WriteLine("🔹 Modern 'using' declaration (C# 8.0+):");
            {
                using var fileManager2 = new FileManager(tempFile2);
                fileManager2.ReadContent();
                Console.WriteLine("Using declaration - no curly braces needed");
            } // fileManager2.Dispose() is called here due to the scope block
            Console.WriteLine("✅ FileManager2 disposed when leaving scope block\n");

            // Clean up our temp files
            File.Delete(tempFile1);
            File.Delete(tempFile2);

            // Pro tip: The compiler transforms this...
            Console.WriteLine("💡 Pro Tip: The compiler transforms the using statement into:");
            Console.WriteLine("   FileManager fm = new FileManager(file);");
            Console.WriteLine("   try {");
            Console.WriteLine("       // your code here");
            Console.WriteLine("   } finally {");
            Console.WriteLine("       if (fm != null) fm.Dispose();");
            Console.WriteLine("   }");
            Console.WriteLine("   This guarantees cleanup even if exceptions occur!");
        }

        /// <summary>
        /// LESSON 2: The three golden rules of disposal that make the .NET world go round.
        /// Follow these and your objects will play nicely with everyone else's.
        /// </summary>
        static void DemonstrateDisposalSemantics()
        {
            Console.WriteLine("Time to learn the three golden rules of disposal:\n");

            string tempFile = Path.GetTempFileName();
            File.WriteAllText(tempFile, "Disposal semantics demo");

            var fileManager = new FileManager(tempFile);

            Console.WriteLine("🔹 RULE 1: Irreversible Disposal");
            Console.WriteLine("Once disposed, an object is 'dead' - no resurrection allowed!");
            fileManager.ReadContent(); // This works
            fileManager.Dispose();     // Object is now disposed

            try
            {
                fileManager.ReadContent(); // This should throw
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine($"✅ Caught expected exception: {ex.Message}");
            }

            Console.WriteLine("\n🔹 RULE 2: Idempotent Disposal");
            Console.WriteLine("Calling Dispose() multiple times should be safe - no errors!");
            fileManager.Dispose(); // First call
            fileManager.Dispose(); // Second call - should be safe
            fileManager.Dispose(); // Third call - still safe
            Console.WriteLine("✅ Called Dispose() three times - no problems!");

            Console.WriteLine("\n🔹 RULE 3: Ownership and Chained Disposal");
            Console.WriteLine("If object X owns object Y, then X.Dispose() should call Y.Dispose()");
            Console.WriteLine("We'll see this in action with our CompositeFileManager next!");

            File.Delete(tempFile);
        }

        /// <summary>
        /// LESSON 3: Understanding the difference between Close() and Dispose().
        /// This trips up a lot of developers, so pay attention!
        /// </summary>
        static void DemonstrateCloseVsDispose()
        {
            Console.WriteLine("Close() vs Dispose() - this is where things get interesting...\n");

            var dbConnection = new DatabaseConnection("Server=localhost;Database=TestDB");

            Console.WriteLine("🔹 Close() - Usually means 'pause' or 'hibernate'");
            dbConnection.Open();
            Console.WriteLine($"Connection state: {dbConnection.State}");

            dbConnection.Close(); // Close the connection
            Console.WriteLine($"After Close(): {dbConnection.State}");

            dbConnection.Open(); // We can reopen it!
            Console.WriteLine($"After reopening: {dbConnection.State}");
            Console.WriteLine("✅ Close() allows the object to be reused\n");

            Console.WriteLine("🔹 Dispose() - Means 'permanent shutdown'");
            dbConnection.Dispose(); // Now it's permanently disposed
            Console.WriteLine($"After Dispose(): {dbConnection.State}");

            try
            {
                dbConnection.Open(); // This should fail
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine($"✅ Cannot reopen after Dispose(): {ex.Message}");
            }

            Console.WriteLine("\n💡 Key Insight:");
            Console.WriteLine("- Close() = Temporary shutdown, can be reopened");
            Console.WriteLine("- Dispose() = Permanent shutdown, object is dead");
            Console.WriteLine("- Some classes make Close() identical to Dispose()");
            Console.WriteLine("- Always check the documentation for the specific class!");
        }

        /// <summary>
        /// LESSON 4: When an object "owns" other disposable objects, it should dispose them too.
        /// This is the third golden rule in action.
        /// </summary>
        static void DemonstrateChainedDisposal()
        {
            Console.WriteLine("Chained disposal - when objects own other objects...\n");

            Console.WriteLine("🔹 Creating a CompositeFileManager that owns two FileManagers:");
            using (var composite = new CompositeFileManager("log1.txt", "log2.txt"))
            {
                composite.WriteToLogs("This is a test log entry");
                Console.WriteLine("CompositeFileManager created and used");
            } // When this disposes, it should dispose both inner FileManagers
            Console.WriteLine("✅ CompositeFileManager disposed - check if inner objects were disposed too!");

            Console.WriteLine("\n💡 This is like a Russian nesting doll:");
            Console.WriteLine("- Outer object disposes");
            Console.WriteLine("- Which disposes inner objects");
            Console.WriteLine("- Which dispose their inner objects");
            Console.WriteLine("- And so on...");
        }

        /// <summary>
        /// LESSON 5: There are times when you should NOT call Dispose().
        /// This goes against the "when in doubt, dispose" rule, but these exceptions matter.
        /// </summary>
        static void DemonstrateWhenNotToDispose()
        {
            Console.WriteLine("When NOT to dispose - the exceptions to the rule...\n");

            Console.WriteLine("🔹 SCENARIO 1: You don't 'own' the object");
            Console.WriteLine("Some objects are shared across the application:");
            Console.WriteLine("Example: System.Drawing.Brushes.Blue is a shared, static resource");
            Console.WriteLine("⚠️  NEVER dispose shared resources like static brushes!");
            Console.WriteLine("✅ Rule: If you got it from a static property, don't dispose it\n");

            Console.WriteLine("🔹 SCENARIO 2: Dispose() does something you want to avoid");
            Console.WriteLine("StreamReader disposes its underlying stream by default:");
            
            var memoryStream = new MemoryStream();
            var writer = new StreamWriter(memoryStream);
            writer.WriteLine("Hello, StreamReader!");
            writer.Flush();
            memoryStream.Position = 0;

            // Use leaveOpen parameter to prevent disposing the stream
            using (var reader = new StreamReader(memoryStream, leaveOpen: true))
            {
                string? content = reader.ReadLine();
                Console.WriteLine($"Read: {content}");
            } // StreamReader disposes, but leaves memoryStream open

            Console.WriteLine($"Stream still usable: {memoryStream.CanRead}");
            memoryStream.Dispose(); // Now we dispose it ourselves
            Console.WriteLine("✅ Used leaveOpen parameter to control disposal\n");

            Console.WriteLine("🔹 SCENARIO 3: The object doesn't really need disposal");
            Console.WriteLine("StringReader/StringWriter don't hold unmanaged resources:");
            var stringReader = new StringReader("Just a string");
            var text = stringReader.ReadToEnd();
            Console.WriteLine($"Read: {text}");
            Console.WriteLine("Could dispose it, but it's not critical - no unmanaged resources");
            stringReader.Dispose(); // We'll dispose it anyway for completeness
            Console.WriteLine("✅ Disposed StringReader - good practice even if not critical");
        }

        /// <summary>
        /// LESSON 6: Dispose() isn't just about unmanaged resources.
        /// Good disposal hygiene includes event unsubscription, flag setting, and data clearing.
        /// </summary>
        static void DemonstrateFieldClearingInDispose()
        {
            Console.WriteLine("Field clearing in Dispose() - the complete cleanup...\n");

            Console.WriteLine("🔹 Event Unsubscription - Preventing Memory Leaks:");
            var eventPublisher = new EventPublisher();
            using (var eventSubscriber = new EventSubscriber(eventPublisher))
            {
                eventPublisher.RaiseTestEvent();
                Console.WriteLine("Subscriber received the event");
            } // Subscriber disposes and unsubscribes
            
            Console.WriteLine("Subscriber disposed - raising event again...");
            eventPublisher.RaiseTestEvent();
            Console.WriteLine("✅ No output from subscriber - it unsubscribed properly\n");

            Console.WriteLine("🔹 Clearing Sensitive Data:");
            using (var secureCache = new SecureCache())
            {
                secureCache.StoreSecret("TopSecretPassword123");
                Console.WriteLine("Secret stored in memory");
            } // Dispose clears the sensitive data
            Console.WriteLine("✅ Sensitive data cleared from memory\n");

            Console.WriteLine("💡 Best Practices for Dispose():");
            Console.WriteLine("1. Unsubscribe from events (prevents memory leaks)");
            Console.WriteLine("2. Set IsDisposed flag (enforces disposal rules)");
            Console.WriteLine("3. Clear sensitive data (security measure)");
            Console.WriteLine("4. Set event handlers to null (prevents unexpected calls)");
            Console.WriteLine("5. Remember: Dispose() is about unmanaged resources first!");
        }

        /// <summary>
        /// LESSON 7: The anonymous disposal pattern - creating IDisposable objects on the fly.
        /// This is incredibly useful for temporary state changes that need guaranteed cleanup.
        /// </summary>
        static void DemonstrateAnonymousDisposal()
        {
            Console.WriteLine("Anonymous disposal pattern - IDisposable on demand...\n");

            var suspendableService = new SuspendableService();
            Console.WriteLine($"Service state: {suspendableService.State}");

            Console.WriteLine("🔹 The old way (error-prone):");
            Console.WriteLine("service.Suspend();");
            Console.WriteLine("try {");
            Console.WriteLine("    // do work");
            Console.WriteLine("} finally {");
            Console.WriteLine("    service.Resume(); // Easy to forget!"); 
            Console.WriteLine("}\n");

            Console.WriteLine("🔹 The new way (bulletproof):");
            Console.WriteLine("Suspending operations with automatic resume...");
            using (suspendableService.SuspendOperations())
            {
                Console.WriteLine($"Inside suspension: {suspendableService.State}");
                Console.WriteLine("Doing some work while suspended...");
                // Even if an exception occurs here, Resume() will be called
            } // Automatically resumes here via Dispose()
            
            Console.WriteLine($"After suspension: {suspendableService.State}");
            Console.WriteLine("✅ Operations resumed automatically!\n");

            Console.WriteLine("💡 The Magic Behind Anonymous Disposal:");
            Console.WriteLine("- Create a helper class that implements IDisposable");
            Console.WriteLine("- Constructor does the 'setup' (like Suspend)");
            Console.WriteLine("- Dispose() does the 'cleanup' (like Resume)");
            Console.WriteLine("- Return it from a method for use in 'using' statements");
            Console.WriteLine("- Bulletproof resource management with minimal code!");
        }
#endregion Chapter 1

#region Chapter 2
        
        /// <summary>
        /// Demonstrates the exact Test() method example from the training material.
        /// This is the fundamental example that shows how automatic memory management works.
        /// </summary>
        static void DemoBasicMemoryAllocation()
        {
            Console.WriteLine("This demonstrates the exact Test() method from the training material:");
            Console.WriteLine("---");
            Console.WriteLine("public void Test()");
            Console.WriteLine("{");
            Console.WriteLine("    byte[] myArray = new byte[1000]; // Memory allocated on the heap");
            Console.WriteLine("    // ... use myArray ...");
            Console.WriteLine("} // Method exits");
            Console.WriteLine("---");
            Console.WriteLine();
            
            Console.WriteLine("Let's trace through what happens:");
            Console.WriteLine("1. Method executes, array allocated on managed heap");
            Console.WriteLine("2. myArray variable (reference) stored on local variable stack");
            Console.WriteLine("3. Array is reachable through myArray reference");
            Console.WriteLine("4. Method completes, myArray pops out of scope");
            Console.WriteLine("5. No active reference points to the array");
            Console.WriteLine("6. Array becomes 'orphaned' or 'unreachable'");
            Console.WriteLine("7. Array is now eligible for garbage collection");
            Console.WriteLine();
            
            Console.WriteLine("Executing the actual Test() method:");
            BasicMemoryExample.Test();
            Console.WriteLine("Method completed - array is now eligible for collection");
            Console.WriteLine();
            
            Console.WriteLine("Key insight: Eligibility ≠ immediate collection!");
            Console.WriteLine("The CLR performs collections periodically based on various factors.");
        }

        /// <summary>
        /// Demonstrates how circular references are handled by the garbage collector.
        /// Shows that circular references don't prevent collection if no roots exist.
        /// This directly addresses the common misconception mentioned in the training material.
        /// </summary>
        static void DemoCircularReferences()
        {
            Console.WriteLine("Creating objects with circular references...");
            Console.WriteLine("This addresses the common misconception that circular references prevent collection");
            Console.WriteLine();

            // Create objects that reference each other in a circle
            CreateCircularReferences();

            Console.WriteLine("Circular reference chain created and became unreachable");
            Console.WriteLine("Despite circular references, objects are eligible for collection");
            Console.WriteLine();
            Console.WriteLine("This is because .NET uses a TRACING garbage collector, not reference counting");
            Console.WriteLine("The GC traces from roots - if no path exists from any root, objects are collected");

            // Show that GC can handle circular references
            long memoryBefore = GC.GetTotalMemory(false);
            Console.WriteLine($"Memory before GC: {memoryBefore:N0} bytes");

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            long memoryAfter = GC.GetTotalMemory(true);
            Console.WriteLine($"Memory after GC: {memoryAfter:N0} bytes");
            Console.WriteLine($"Memory reclaimed: {memoryBefore - memoryAfter:N0} bytes");
            Console.WriteLine("✓ Circular references were successfully collected!");
            Console.WriteLine();
            Console.WriteLine("Key insight: Circular references are not a problem in .NET");
            Console.WriteLine("The GC only collects objects that cannot be reached from roots");
        }

        /// <summary>
        /// Creates a chain of objects with circular references but no root reference.
        /// </summary>
        static void CreateCircularReferences()
        {
            var obj1 = new CircularReferenceObject("Object 1");
            var obj2 = new CircularReferenceObject("Object 2");
            var obj3 = new CircularReferenceObject("Object 3");

            // Create circular references
            obj1.Reference = obj2;
            obj2.Reference = obj3;
            obj3.Reference = obj1; // Completes the circle

            Console.WriteLine("Created circular reference: obj1 → obj2 → obj3 → obj1");

            // No root variables store references after method exits
            // Despite circular references, all objects become unreachable
        }

        /// <summary>
        /// Demonstrates the Large Object Heap (LOH) behavior.
        /// Objects >= 85KB go to LOH and have different collection characteristics.
        /// </summary>
        static void DemoLargeObjectHeap()
        {
            Console.WriteLine("Exploring Large Object Heap (LOH) behavior...");
            Console.WriteLine("Objects >= 85KB are allocated on the Large Object Heap");
            Console.WriteLine("LOH has different collection characteristics than regular heap");
            Console.WriteLine();
            
            const int smallObjectSize = 1000;      // 1KB - goes to regular heap
            const int largeObjectSize = 100_000;   // 100KB - goes to LOH

            Console.WriteLine($"Creating small objects ({smallObjectSize} bytes each)...");
            var smallObjects = new List<byte[]>();
            
            for (int i = 0; i < 100; i++)
            {
                smallObjects.Add(new byte[smallObjectSize]);
            }

            Console.WriteLine($"Creating large objects ({largeObjectSize} bytes each)...");
            var largeObjects = new List<byte[]>();
            
            for (int i = 0; i < 20; i++)
            {
                largeObjects.Add(new byte[largeObjectSize]);
            }

            Console.WriteLine("Large objects (>=85KB) are allocated on the Large Object Heap");
            Console.WriteLine("LOH is collected less frequently and only during Gen 2 collections");

            PrintGCGenerationInfo();

            // Clear small objects first
            Console.WriteLine("\nClearing small objects and forcing Gen 0/1 collection...");
            smallObjects.Clear();
            GC.Collect(1); // Collect Gen 0 and 1 only

            Console.WriteLine("After Gen 0/1 collection (LOH objects should remain):");
            PrintGCGenerationInfo();

            // Clear large objects and force full collection
            Console.WriteLine("\nClearing large objects and forcing full collection...");
            largeObjects.Clear();
            GC.Collect(); // Full collection includes LOH

            Console.WriteLine("After full collection (LOH objects should be collected):");
            PrintGCGenerationInfo();
        }

        /// <summary>
        /// Demonstrates memory pressure and what triggers garbage collection.
        /// Shows how allocation patterns affect GC behavior.
        /// </summary>
        static void DemoMemoryPressure()
        {
            Console.WriteLine("Demonstrating memory pressure and GC triggers...");
            Console.WriteLine("This shows the three factors mentioned in the training material:");
            Console.WriteLine("1. Available Memory");
            Console.WriteLine("2. Amount of Memory Allocation");
            Console.WriteLine("3. Time Since Last Collection");
            Console.WriteLine();

            // Monitor collection counts
            int gen0Before = GC.CollectionCount(0);
            int gen1Before = GC.CollectionCount(1);
            int gen2Before = GC.CollectionCount(2);

            Console.WriteLine("Creating memory pressure through rapid allocation...");

            // Create memory pressure by rapidly allocating and abandoning objects
            for (int i = 0; i < 10000; i++)
            {
                // Rapid allocation creates pressure on Generation 0
                var tempArray = new byte[1000];
                var tempString = new string('x', 100);
                
                // Every 2000 iterations, show current state
                if (i % 2000 == 0 && i > 0)
                {
                    int gen0Current = GC.CollectionCount(0);
                    Console.WriteLine($"After {i} allocations: Gen 0 collections: {gen0Current - gen0Before}");
                }
            }

            // Show final collection counts
            Console.WriteLine("\nFinal garbage collection statistics:");
            Console.WriteLine($"Gen 0 collections triggered: {GC.CollectionCount(0) - gen0Before}");
            Console.WriteLine($"Gen 1 collections triggered: {GC.CollectionCount(1) - gen1Before}");
            Console.WriteLine($"Gen 2 collections triggered: {GC.CollectionCount(2) - gen2Before}");

            Console.WriteLine("\nKey insights:");
            Console.WriteLine("- Rapid allocation triggers frequent Gen 0 collections");
            Console.WriteLine("- Gen 1 collections happen less frequently");
            Console.WriteLine("- Gen 2 collections are rare and expensive");
            Console.WriteLine("- The GC dynamically self-tunes based on these patterns");
        }

        /// <summary>
        /// Demonstrates weak references and their behavior with garbage collection.
        /// Shows how weak references don't keep objects alive.
        /// </summary>
        static void DemoWeakReferences()
        {
            Console.WriteLine("Exploring weak references and their behavior...");

            // Create an object with both strong and weak references
            var strongRef = new SampleObject("Weakly Referenced Object");
            var weakRef = new WeakReference(strongRef);

            Console.WriteLine($"Created object with strong and weak references");
            Console.WriteLine($"Strong reference: {strongRef.Name}");
            Console.WriteLine($"Weak reference target: {(weakRef.Target as SampleObject)?.Name ?? "null"}");
            Console.WriteLine($"Weak reference is alive: {weakRef.IsAlive}");

            // Remove strong reference but keep weak reference
            Console.WriteLine("\nRemoving strong reference...");
            strongRef = null!;

            // Object might still be alive (GC hasn't run yet)
            Console.WriteLine($"Weak reference is alive: {weakRef.IsAlive}");
            Console.WriteLine($"Weak reference target: {(weakRef.Target as SampleObject)?.Name ?? "null"}");

            // Force garbage collection
            Console.WriteLine("Forcing garbage collection...");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // Now the weak reference should be dead
            Console.WriteLine($"After GC - Weak reference is alive: {weakRef.IsAlive}");
            Console.WriteLine($"Weak reference target: {(weakRef.Target as SampleObject)?.Name ?? "null"}");

            Console.WriteLine("\nKey insight: Weak references don't prevent garbage collection");
            Console.WriteLine("They're useful for caches and observers that shouldn't keep objects alive");
        }

        /// <summary>
        /// Demonstrates finalization patterns and the interaction between GC and IDisposable.
        /// Shows the overhead of finalization and benefits of proper disposal.
        /// </summary>
        static void DemoFinalizationAndDisposal()
        {
            Console.WriteLine("Exploring finalization and disposal patterns...");

            // Demo proper disposal
            FinalizationExample.DemoProperDisposal();

            // Demo finalization impact
            FinalizationExample.DemoFinalizationImpact();

            Console.WriteLine("\nKey insights about finalization:");
            Console.WriteLine("1. Objects with finalizers require TWO GC cycles to be fully collected");
            Console.WriteLine("2. Finalizers add significant overhead to garbage collection");
            Console.WriteLine("3. Always implement IDisposable for deterministic cleanup");
            Console.WriteLine("4. Use 'using' statements or call Dispose() explicitly");
            Console.WriteLine("5. Call GC.SuppressFinalize() in Dispose() to avoid finalizer overhead");
        }

        /// <summary>
        /// Shows information about garbage collection across all generations.
        /// </summary>
        static void PrintGCGenerationInfo()
        {
            Console.WriteLine($"  Gen 0 collections: {GC.CollectionCount(0)}");
            Console.WriteLine($"  Gen 1 collections: {GC.CollectionCount(1)}");
            Console.WriteLine($"  Gen 2 collections: {GC.CollectionCount(2)}");
            Console.WriteLine($"  Total memory: {GC.GetTotalMemory(false):N0} bytes");
        }
#endregion Chapter 2

#region Chapter 3

        static void WaitForUser()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine();
        }

        /// <summary>
        /// Demonstrates basic finalizer behavior and execution.
        /// Shows the fundamental syntax and when finalizers get called.
        /// </summary>
        static void DemoBasicFinalizer()
        {
            Console.WriteLine("Creating objects with finalizers...");

            // Create some objects with finalizers in a local scope
            CreateFinalizableObjects();

            Console.WriteLine("Objects went out of scope - now eligible for collection");
            Console.WriteLine("But finalizers haven't run yet...");

            // Force garbage collection to trigger finalizers
            Console.WriteLine("Forcing garbage collection...");
            GC.Collect();
            GC.WaitForPendingFinalizers(); // Wait for finalizers to complete
            GC.Collect(); // Second collection to actually free the memory

            Console.WriteLine("Finalizers should have executed by now");
        }

        /// <summary>
        /// Helper method that creates objects with finalizers in a local scope.
        /// When this method exits, the objects become eligible for garbage collection.
        /// </summary>
        static void CreateFinalizableObjects()
        {
            var obj1 = new BasicFinalizerExample("Object1");
            var obj2 = new BasicFinalizerExample("Object2");
            var obj3 = new BasicFinalizerExample("Object3");

            Console.WriteLine("Created 3 objects with finalizers");
            // Objects become unreachable when method exits
        }

        /// <summary>
        /// Demonstrates the multi-phase garbage collection process with finalizers.
        /// Shows how GC handles objects with finalizers differently.
        /// </summary>
        static void DemoFinalizerPhases()
        {
            Console.WriteLine("Understanding GC phases with finalizers:");
            Console.WriteLine("Phase 1: Mark objects for collection");
            Console.WriteLine("Phase 2: Objects with finalizers go to finalization queue");
            Console.WriteLine("Phase 3: Finalizer thread runs finalizers");
            Console.WriteLine("Phase 4: Next GC cycle actually frees memory");
            Console.WriteLine();

            Console.WriteLine("Creating objects to demonstrate phases...");
            for (int i = 0; i < 3; i++)
            {
                new GCPhaseDemo($"Phase_Object_{i}");
            }

            Console.WriteLine("\nStep 1: First GC.Collect() - objects marked for finalization");
            GC.Collect();
            
            Console.WriteLine("\nStep 2: WaitForPendingFinalizers() - finalizers execute");
            GC.WaitForPendingFinalizers();
            
            Console.WriteLine("\nStep 3: Second GC.Collect() - memory actually freed");
            GC.Collect();
            
            Console.WriteLine("\nThis two-phase process is why finalizers have performance overhead!");
        }

        /// <summary>
        /// Demonstrates the performance impact of having finalizers.
        /// Shows how objects with finalizers live longer and impact GC performance.
        /// </summary>
        static void DemoPerformanceImpact()
        {
            Console.WriteLine("Comparing performance: objects with vs without finalizers");

            // Measure time to create and collect objects WITHOUT finalizers
            var stopwatch = Stopwatch.StartNew();
            CreateObjectsWithoutFinalizers(1000);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            stopwatch.Stop();

            long timeWithoutFinalizers = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Time for 1000 objects WITHOUT finalizers: {timeWithoutFinalizers} ms");

            // Measure time to create and collect objects WITH finalizers
            stopwatch.Restart();
            CreateObjectsWithFinalizers(1000);
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            stopwatch.Stop();

            long timeWithFinalizers = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"Time for 1000 objects WITH finalizers: {timeWithFinalizers} ms");

            Console.WriteLine($"Performance difference: {timeWithFinalizers - timeWithoutFinalizers} ms slower");
            Console.WriteLine("Objects with finalizers require TWO garbage collection cycles!");
        }

        /// <summary>
        /// Creates objects without finalizers for performance comparison.
        /// </summary>
        static void CreateObjectsWithoutFinalizers(int count)
        {
            for (int i = 0; i < count; i++)
            {
                new SimpleObject($"NoFinalizer_{i}");
            }
        }

        /// <summary>
        /// Creates objects with finalizers for performance comparison.
        /// </summary>
        static void CreateObjectsWithFinalizers(int count)
        {
            for (int i = 0; i < count; i++)
            {
                new BasicFinalizerExample($"WithFinalizer_{i}");
            }
        }

        /// <summary>
        /// Demonstrates the proper Dispose pattern with finalizers.
        /// Shows how finalizers act as a safety net when Dispose isn't called.
        /// </summary>
        static void DemoDisposePattern()
        {
            Console.WriteLine("Demonstrating the Dispose pattern with finalizers:");
            Console.WriteLine();

            // Scenario 1: Proper disposal - finalizer suppressed
            Console.WriteLine("Scenario 1: Proper disposal");
            using (var properDisposal = new ProperDisposalExample("ProperlyDisposed"))
            {
                properDisposal.DoWork();
            } // Dispose called automatically here
            
            Console.WriteLine();

            // Scenario 2: Improper disposal - finalizer runs as safety net
            Console.WriteLine("Scenario 2: Improper disposal (finalizer as safety net)");
            var improperDisposal = new ProperDisposalExample("ImproperlyDisposed");
            improperDisposal.DoWork();
            improperDisposal = null; // Not disposed properly!
            
            Console.WriteLine("Forcing GC to show finalizer safety net...");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        /// <summary>
        /// Demonstrates object resurrection - when finalizers make objects reachable again.
        /// Shows how objects can "come back to life" during finalization.
        /// </summary>
        static void DemoObjectResurrection()
        {
            Console.WriteLine("Demonstrating object resurrection:");
            Console.WriteLine("When finalizers make objects reachable again...");
            Console.WriteLine();

            // Create a temporary file that might fail to delete
            var tempFile = new TempFileRef("test_file.tmp");
            tempFile = null; // Make it eligible for collection

            Console.WriteLine("Object eligible for collection...");
            Console.WriteLine("Forcing GC - finalizer will try to delete file...");
            
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            // Check if any objects were resurrected
            Console.WriteLine($"Objects in failed deletion queue: {TempFileRef.FailedDeletions.Count}");
            
            // Try to process failed deletions
            Console.WriteLine("Processing failed deletions...");
            TempFileRef.ProcessFailedDeletions();
        }

        /// <summary>
        /// Demonstrates GC.ReRegisterForFinalize for retry scenarios.
        /// Shows how to make finalizers run multiple times.
        /// </summary>
        static void DemoReRegisterForFinalize()
        {
            Console.WriteLine("Demonstrating GC.ReRegisterForFinalize:");
            Console.WriteLine("Making finalizers run multiple times for retry scenarios...");
            Console.WriteLine();

            var retryFile = new RetryTempFileRef("retry_file.tmp");
            retryFile = null; // Make it eligible for collection

            Console.WriteLine("Forcing multiple GC cycles to show retry mechanism...");
            
            // Force multiple GC cycles to demonstrate retry
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"GC cycle {i + 1}:");
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                Thread.Sleep(100); // Small delay between cycles
            }
        }

        /// <summary>
        /// Demonstrates common mistakes people make with finalizers.
        /// Shows what NOT to do when implementing finalizers.
        /// </summary>
        static void DemoCommonMistakes()
        {
            Console.WriteLine("WARNING: The following examples show BAD practices!");
            Console.WriteLine("We're demonstrating what NOT to do with finalizers");
            Console.WriteLine();

            // Note: In a real application, you would never implement finalizers like this
            Console.WriteLine("Creating object with poorly implemented finalizer...");

            var badExample = new BadFinalizerExample("BadImplementation");
            badExample = null!;

            Console.WriteLine("Forcing GC to show problems with bad finalizer implementation...");

            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during finalization: {ex.Message}");
            }

            Console.WriteLine("The bad finalizer demonstrates common mistakes:");
            Console.WriteLine("- Taking too long to execute");
            Console.WriteLine("- Potentially throwing exceptions");
            Console.WriteLine("- Trying to access other objects");
            Console.WriteLine("- Performing complex operations");
        }

        /// <summary>
        /// Demonstrates issues with finalizer execution order and object dependencies.
        /// Shows why finalizers shouldn't access other finalizable objects.
        /// </summary>
        static void DemoFinalizerOrder()
        {
            Console.WriteLine("Demonstrating finalizer order unpredictability:");
            Console.WriteLine("You cannot predict the order in which finalizers execute!");
            Console.WriteLine();

            // Create interdependent objects to show order issues
            var container = new FinalizerContainer("Container");
            var item1 = new FinalizerItem("Item1");
            var item2 = new FinalizerItem("Item2");
            var item3 = new FinalizerItem("Item3");

            container.AddItem(item1);
            container.AddItem(item2);
            container.AddItem(item3);

            // Clear references
            container = null;
            item1 = null;
            item2 = null;
            item3 = null;

            Console.WriteLine("All references cleared - watch the finalization order:");
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            Console.WriteLine("Notice: The order is unpredictable!");
            Console.WriteLine("This is why finalizers shouldn't depend on each other.");
        }
#endregion Chapter 3

#region Chapter 4
#endregion Chapter 4

#region Chapter 5
#endregion Chapter 5

#region Chapter 6
#endregion Chapter 6

#region Chapter 7
#endregion Chapter 7

#region Chapter 8
#endregion Chapter 8
}

/// <summary>
/// Simple object without finalizer for performance comparison.
/// Used to demonstrate the performance difference between objects with and without finalizers.
/// </summary>
public class SimpleObject
{
    private readonly string _name;

    public SimpleObject(string name)
    {
        _name = name;
    }
}
