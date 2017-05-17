// TreeTraversal.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <iostream>
#include <stack>
using namespace std;
/* you only have to complete the function given below.
Node is defined as
*/
struct node
{
int data;
node* left;
node* right;
};



void preOrdRec(node* n) {
	if (n == nullptr)
		return;
	std::cout << n->data << " ";
	preOrdRec(n->left);

	preOrdRec(n->right);
}

void ThreadedTraversal(node* n) {
	//node* current = n;

	while (n != nullptr) {

		node* temp = nullptr;
		if (n->left != nullptr) {
			temp = n->left;
			while (temp->right != nullptr && temp->right != n) {
				temp = temp->right;
			}
			if (temp->right == nullptr) {
				cout << n->data << " ";
				temp->right = n;
				n = n->left;
			}
			else {
				temp->right = nullptr;
				n = n->right;
			}

		}
		else {
			cout << n->data << " ";
			n = n->right;
		}
	}
}

void preOrdStack(node* n) {
	stack<node*> q;
	q.push(n);

	while (!q.empty()) {
		auto n = q.top();
		q.pop();
		cout << n->data << ' ';
		if (n->right != nullptr)
			q.push(n->right);
		if (n->left != nullptr)
			q.push(n->left);
	}
}

void preOrder(node *root) {
	ThreadedTraversal(root);
}



int main()
{
    return 0;
}

