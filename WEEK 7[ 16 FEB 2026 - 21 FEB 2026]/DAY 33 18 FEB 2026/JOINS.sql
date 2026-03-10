CREATE TABLE Customers(
    customer_id INT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    email VARCHAR(100),
    phone VARCHAR(20),
    city VARCHAR(50),
    state VARCHAR(50)
);

CREATE TABLE PolicyCategories(
    category_id INT PRIMARY KEY,
    category_name VARCHAR(50)
);

CREATE TABLE Policies(
    policy_id INT PRIMARY KEY,
    category_id INT,
    policy_name VARCHAR(50),
    yearly_premium FLOAT,
    duration_years INT,
    maturity_amount FLOAT,
    FOREIGN KEY (category_id) REFERENCES PolicyCategories(category_id)
);

CREATE TABLE CustomerPolicies(
    customer_policy_id INT PRIMARY KEY,
    customer_id INT,
    policy_id INT,
    start_date DATE,
    FOREIGN KEY (customer_id) REFERENCES Customers(customer_id),
    FOREIGN KEY (policy_id) REFERENCES Policies(policy_id)
);

CREATE TABLE Payments(
    payment_id INT PRIMARY KEY,
    customer_policy_id INT,
    payment_date DATE,
    amount_paid FLOAT,
    FOREIGN KEY (customer_policy_id) REFERENCES CustomerPolicies(customer_policy_id)
);

/*INNER JOIN*/
SELECT c.customer_id,
       c.first_name,
       p.policy_name
FROM Customers c
INNER JOIN CustomerPolicies cp
       ON c.customer_id = cp.customer_id
INNER JOIN Policies p
       ON cp.policy_id = p.policy_id;

/*LEFT JOIN*/
SELECT c.customer_id,
       c.first_name,
       p.policy_name
FROM Customers c
LEFT JOIN CustomerPolicies cp
       ON c.customer_id = cp.customer_id
LEFT JOIN Policies p
       ON cp.policy_id = p.policy_id;

/*RIGHT JOIN*/
SELECT c.customer_id,
       c.first_name,
       p.policy_name
FROM Customers c
RIGHT JOIN CustomerPolicies cp
       ON c.customer_id = cp.customer_id
RIGHT JOIN Policies p
       ON cp.policy_id = p.policy_id;

/*FULL OUTER JOIN*/
SELECT c.customer_id,
       c.first_name,
       p.policy_name
FROM Customers c
FULL OUTER JOIN CustomerPolicies cp
       ON c.customer_id = cp.customer_id
FULL OUTER JOIN Policies p
       ON cp.policy_id = p.policy_id;

/*CROSS JOIN*/
SELECT c.first_name,
       pc.category_name
FROM Customers c
CROSS JOIN PolicyCategories pc;

/*SELF JOIN*/
SELECT c1.first_name AS Customer1,
       c2.first_name AS Customer2,
       c1.city
FROM Customers c1
JOIN Customers c2
     ON c1.city = c2.city
     AND c1.customer_id <> c2.customer_id;


