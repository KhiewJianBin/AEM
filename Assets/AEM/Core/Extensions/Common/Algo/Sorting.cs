public enum SortingType { Ascending, Descending }

public static class Sorting
{
    /// <summary>
    /// Insertion Sorting Algorithm Long Way
    /// </summary>
    /// <param name="SortType">Ascending or Descending</param>
    /// <param name="in_array">Array of Numbers to sort</param>
    public static void Insertion(SortingType SortType, int[] in_array)
    {
        //starting with the second element,compare it with the previous element and swap if nessasary continue up the array until the last index is compared
        //if there is a swap, set the current swap index as the prev swap index
        for (int i = 1; i < in_array.Length;)// i is the current array index, i = 1 indicating the second element
        {
            int j = i;// j is the current swap index

            #region Ascending
            if (SortType == SortingType.Ascending) // If Ascending 
            {
                while (j > 0 && in_array[j - 1] > in_array[j]) //compare current index (j) with the previous index (j-1) if previous is bigger(previous should be smaller)
                {
                    int swap1 = in_array[j];
                    int swap2 = in_array[j - 1];

                    in_array[j] = swap2;
                    in_array[j - 1] = swap1;

                    j = j - 1; //set current swap index as the prev index
                }
            }
            #endregion

            #region Descending
            else if (SortType == SortingType.Descending) // If Descending 
            {
                while (j > 0 & in_array[j - 1] < in_array[j]) // Same as Ascending, just comparing which is smaller (previous should be bigger)
                {
                    int swap1 = in_array[j];
                    int swap2 = in_array[j - 1];

                    in_array[j] = swap2;
                    in_array[j - 1] = swap1;

                    j = j - 1;
                }
            }
            i++;
            #endregion
        }
    }
    /// <summary>
    /// Insertion Sorting Algorithm Short Way
    /// </summary>
    /// <param name="SortType">Ascending or Descending</param>
    /// <param name="in_array">Array of Numbers to sort</param>
    public static void Insertion2(SortingType SortType, int[] in_array)
    {
        // Description : Insertion sorting algorithm 2
        // This Algo have an extra step that makes it a little more faster 
        // by remembering the the contents and putting into position rather than swaping the contents each time it compares
        for (int i = 1; i < in_array.Length; i++)
        {
            int c = in_array[i]; // Store the contents of current index
            int j = i;

            #region Ascending
            if (SortType == SortingType.Ascending)
            {
                while (j > 0 & in_array[j - 1] > c) // Check if the prev index(j - 1) is bigger than the stored content (c)
                {
                    in_array[j] = in_array[j - 1]; //Push up contents of the prev index to current index

                    j = j - 1; //set current swap index as the prev index
                }
                in_array[j] = c; // If previous index is not bigger than the stored content (c), than means the current index should contain the stored content (c)
            }
            #endregion

            #region Descending
            else if (SortType == SortingType.Descending)
            {
                while (j > 0 & in_array[j - 1] < c) // Same as Ascending, just reversed check
                {
                    in_array[j] = in_array[j - 1];

                    j = j - 1;

                    if (j <= 0)
                    {
                        break;
                    }
                }
                in_array[j] = c;
            }
            #endregion
        }
    }
    /// <summary>
    /// Selection Sorting Algorithm
    /// </summary>
    /// <param name="SortType">Ascending or Descending</param>
    /// <param name="in_array">Array of Numbers to sort</param>
    public static void Selection(SortingType SortType, int[] in_array)
    {
        //start with first index and compare it with every other element and swap with the current index
        for (int i = 0; i < in_array.Length;)
        {
            #region Ascending
            if (SortType == SortingType.Ascending)
            {
                int smallest;
                smallest = i; // lets first put the first element currently as the smallest number
                for (int j = i + 1; j < in_array.Length;)
                {
                    if (in_array[j] < in_array[smallest])
                    {
                        smallest = j; // find the smallest among the rest of the numbers
                    }
                    j++;
                }
                int swap1 = in_array[i];
                int swap2 = in_array[smallest];

                in_array[i] = swap2; // swap the current index with smallest
                in_array[smallest] = swap1;
            }
            #endregion

            #region Desecnding
            else if (SortType == SortingType.Descending)
            {
                int biggest;
                biggest = 0; // lets first put the first element currently as the biggest number
                for (int j = i + 1; j < in_array.Length;)
                {
                    if (in_array[j] > in_array[biggest])
                    {
                        biggest = j; // find the biggest among the rest of the numbers
                    }
                    j++;
                }
                int swap1 = in_array[i];
                int swap2 = in_array[biggest];

                in_array[i] = swap2; // swap the current index with smallest
                in_array[biggest] = swap1;
            }
            #endregion

            i++;
        }

    }


    
    /// <summary>
    /// Merge Sort TopDown Algorithm
    /// </summary>
    /// <param name="arrayA">Items to sort</param>
    /// <param name="arrayB">Work array</param>
    /// <param name="n"></param>
    public static void TopDownMergeSort(int[] arrayA, int[] arrayB, int n)
    {
        TopDownSplitMerge(arrayA, 0, n, arrayB);
    }
    static void TopDownSplitMerge(int[] arrayA, int iBegin, int iEnd, int[] arrayB)
    {
        if (iEnd - iBegin < 2)
        {
            // if run size == 1 return; 
            // consider it sorted 
            // recursively split runs into two halves until run size == 1, 
            // then merge them and return back up the call chain 
            
            int iMiddle = (iEnd + iBegin) / 2; // iMiddle = mid point 

            TopDownSplitMerge(arrayA, iBegin, iMiddle, arrayB); // split / merge left half 
            TopDownSplitMerge(arrayA, iMiddle, iEnd, arrayB); // split / merge right half 

            TopDownMerge(arrayA, iBegin, iMiddle, iEnd, arrayB); // merge the two half runs 
            CopyArray(arrayA, iBegin, iEnd, arrayA); // copy the merged runs back to A 
        }
    } // left half is A[iBegin :iMiddle-1] // right half is A[iMiddle:iEnd-1 ] 


    // iBegin is inclusive; iEnd is exclusive (A[iEnd] is not in the set) 

    static void TopDownMerge(int[] arrayA, int iBegin,int iMiddle,int iEnd, int[] arrayB)
    {
        int i0 = iBegin;// While there are elements in the left or right runs 
        int i1 = iMiddle; 

        for (int j = iBegin; j < iEnd; j++)
        {
            // If left run head exists and is <= existing right run head. 
            if (i0 < iMiddle && (i1 >= iEnd || arrayA[i0] <= arrayA[i1]))
            {
                arrayB[j] = arrayA[i0]; i0 = i0 + 1;
            }
            else
            {
                arrayB[j] = arrayA[i1]; i1 = i1 + 1;
            }
        }
    }

    static void CopyArray(int[] arrayA, int iBegin, int iEnd, int[] arrayB)
    {
        for (int k = iBegin; k < iEnd;)
        {
            arrayA[k] = arrayB[k];

            k++;
        }
    }

    /* array A[] has the items to sort; array B[] is a work array */
    //void BottomUpMergeSort(A[], B[], n) { /* Each 1-element run in A is already "sorted". */ /* Make successively longer sorted runs of length 2, 4, 8, 16... until whole array is sorted. */ for (width = 1; width < n; width = 2 * width) { /* Array A is full of runs of length width. */ for (i = 0; i < n; i = i + 2 * width) { /* Merge two runs: A[i:i+width-1] and A[i+width:i+2*width-1] to B[] */ /* or copy A[i:n-1] to B[] ( if(i+width >= n) ) */ BottomUpMerge(A, i, min(i + width, n), min(i + 2 * width, n), B); } /* Now work array B is full of runs of length 2*width. */ /* Copy array B to array A for next iteration. */ /* A more efficient implementation would swap the roles of A and B */ CopyArray(B, A, n); /* Now array A is full of runs of length 2*width. */ } }
    //void BottomUpMerge(A[], iLeft, iRight, iEnd, B[]) { i0 = iLeft; i1 = iRight; j; /* While there are elements in the left or right runs */ for (j = iLeft; j < iEnd; j++) { /* If left run head exists and is <= existing right run head */ if (i0 < iRight && (i1 >= iEnd ---- A[i0] <= A[i1])) { B[j] = A[i0]; i0 = i0 + 1; } else { B[j] = A[i1]; i1 = i1 + 1; } } }
    //void CopyArray(B[], A[], n) { for (i = 0; i < n; i++) A[i] = B[i]; }
}
