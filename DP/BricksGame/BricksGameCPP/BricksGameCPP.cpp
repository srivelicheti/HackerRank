// BricksGameCPP.cpp : Defines the entry point for the console application.
//

#include "stdafx.h";
#include <iostream>;
#include <vector>;
#include <algorithm>;
using namespace std;
typedef long long ull;

ull Sum(vector<ull>& v)
{
	ull sum = 0;
	for (int i(v.size()); i > 0; --i)
		sum += v[i - 1];

	return sum;
}

ull Min(ull i1, ull i2, ull i3)
{
	ull min = i1;
	min = i1 > i2 ? i2 : i1;
	min = min > i3 ? i3 : min;
	return min;
}
int main()
{
	int n;
	std::cin >> n;

	while (n--)
	{
		ull l;
		std::cin >> l;
		ull temp = l;
		const int zz = l;
		vector<ull> arr ( l , 0 );
		vector<ull> res( l, 0 );
		vector<ull> bCount( l, 0 );
		while (temp != 0)
		{
			std::cin >> arr[temp -1];
			temp--;
			
		}

		if (l <= 3){
			std::cout << Sum(arr) << endl;
			continue;
		}
		res[0] = arr[0];
		res[1] = res[0] + arr[1];
		res[2] = res[1] + arr[2];
		res[3] = res[2] - arr[0] + arr[3];
		bCount[0] = 1;
		bCount[1] = 2;
		bCount[2] = bCount[3] = 3;
		for (int i = 4; i < l; i++)
		{
			ull b1 = arr[i] + (i - 1 - bCount[i - 1] >= 0 ? res[i - 1 - bCount[i - 1]] : 0);
			ull b2 = arr[i] + arr[i - 1] + (i - 2 - bCount[i - 2] >= 0 ? res[i - 2 - bCount[i - 2]] : 0);
			ull b3 = arr[i] + arr[i - 1] + arr[i - 2] + (i - 3 - bCount[i - 3] >= 0 ? res[i - 3 - bCount[i - 3]] : 0);

			ull max = b1;
			bCount[i] = 1;
			if (b2 > max)
			{
				max = b2;
				bCount[i] = 2;
			}
			if (b3 > max)
			{
				max = b3;
				bCount[i] = 3;
			}

			res[i] = max;
			
		}
		cout << res[l - 1] << endl;

	}
	std::cout << "Completed" << endl;
	int xxxx;
	std::cin >> xxxx;
	return 0;
}

