﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphApp.Extentions
{
    public static class ArrayExtentions
    {
        public static int MaxValueIndex(this int[] nums)
        {
            var max = nums[0];
            var maxIndex = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                if (max < nums[i])
                {
                    maxIndex = i;
                    max = nums[i];
                }
            }

            return maxIndex;
        }

        public static int MinValueIndex(this int[] nums)
        {
            var min = nums[0];
            var minIndex = 0;
            for(int i = 0; i < nums.Length;i++)
            {
                if (min > nums[i]) 
                { 
                    minIndex = i;
                    min = nums[i];
                }
            }

            return minIndex;
        }

    }
}
