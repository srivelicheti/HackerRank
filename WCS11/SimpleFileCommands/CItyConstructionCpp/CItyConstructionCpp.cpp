// CItyConstructionCpp.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <unordered_map>
#include <iostream>
#include <unordered_map>
#include <unordered_set>
#include <map>
using namespace std;

void AddEdge(int u, int v, map<int, unordered_set<int>>& city, map<int, unordered_set<int>>& inci)
{
	bool edgeAdded = true;

	auto find = city.find(u);

	if(find == city.end())
	{
		unordered_set<int> temp={};
		temp.insert(v);
		city.insert({ u,temp });
	}
	else 
	{
		auto temp = find->second.find(v);
		if (temp == find->second.end()) {
			find->second.insert(v);
		}
		else
			edgeAdded = false;
	}

	if(edgeAdded)
	{
		auto temp2 = city.find(v);
		if(temp2 != city.end())
		{
			for (auto i : temp2->second)
			{
				AddEdge(u, i, city, inci);
			}
		}

		auto temp3 = inci.find(v);
		if(temp3 == inci.end())
		{
			unordered_set<int> temp = {};
			temp.insert(u);
			inci.insert({ v,temp });
		}
		else
		{
			temp3->second.insert(u);
		}

		auto temp4 = inci.find(u);
		if(temp4 != inci.end())
		{
			for (auto x : temp4->second)
			{
				AddEdge(x, v, city, inci);
			}
		}
	}
}

int main()
{
	int n;
	std::cin >> n;
	int m;
	cin >> m;

	map<int, unordered_set<int>> city = {};
	map<int, unordered_set<int>> inci = {};
	while (m--)
	{
		int u;
		int v;
		cin >> u;
		cin >> v;
		AddEdge(u, v, city, inci);

	}

	int q;
	cin >> q;

	while(q--)
	{
		int z;
		cin >> z;
		
		if(z == 1)
		{
			auto newCity = ++n;
			int existing;
			cin >> existing;
			int p;
			cin >> p;
			if (p == 0)
			{
				AddEdge(existing, newCity, city, inci);
			}
			else
			{
				AddEdge(newCity, existing, city, inci);
			}
		}
		else
		{
			int u;
			int v;
			cin >> u;
			cin >> v;

			auto temp6 = city.find(u);
			if (temp6 != city.end())
			{
				if (temp6->second.find(v) != temp6->second.end())
				{
					cout << "Yes" << endl;
				}
				else
					cout << "No" << endl;
			}
			else
				cout << "No" << endl;
		}
	}

    return 0;
}

