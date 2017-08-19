// TimeSeriesQueries.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <map>
#include <vector>
#include <algorithm>
#include <cmath>
#include <fstream>

typedef long long ll;

using namespace std;

int main()
{
#if _DEBUG
	std::ifstream ins("input.txt");
	std::streambuf *cinbuf = std::cin.rdbuf(); //save old buf
	std::cin.rdbuf(ins.rdbuf()); //redirect std::cin to in.txt!
#endif
	int n, q;
	cin >> n >> q;
	
	auto ts = new ll[n];
	int i = 0;
	while(i<n)
	{
		cin >> ts[i];
		i++;
	}
	i = 0;
	auto price = new ll[n];

	while (i<n)
	{
		cin >> price[i];
		i++;
	}

	i = n - 1;
	ll curMax = price[i];
	auto maxArr = new ll[n];
	maxArr[i] = curMax;
	i--;
	while(i>=0)
	{
		if (price[i] > curMax)
			curMax = price[i];
		maxArr[i] = curMax;
		i--;
	}

	map<ll, ll> pMap;
	map<ll, ll> tsMap;
	i = 0;
	while(i<n)
	{
		pMap.insert({ price[i],i });
		i++;
	}

	i = 0;
	while (i<n)
	{
		tsMap.insert({ ts[i],i });
		i++;
	}

	while(q--)
	{
		int t, v;
		cin >> t >> v;
		if(t == 1)
		{
			auto index = pMap.find(v);
			if (index == pMap.end()) {
			  index = pMap.upper_bound(v);
			}
			if(index == pMap.end())
			{
				cout << -1 << endl;
			}
			else
			{
				ll minTs = ts[index->second];
				for(i=0;i<index->second;i++)
				{
					if(price[i]>=v)
					{
						minTs = ts[i];
						break;
					}
				}
				cout << minTs << endl;
			}

		}
		else
		{
			auto index = tsMap.find(v);
			if (index == tsMap.end()) {
				index = tsMap.upper_bound(v);
			}
			if (index == tsMap.end())
			{
				cout << -1 << endl;
			}
			else
			{
				cout << maxArr[index->second] << endl;
				//cout << FindMax(index->second, n - 1, 0, n - 1, sg, 0) << endl;
			}
		}
	}

    return 0;
}

