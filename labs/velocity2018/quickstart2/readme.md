# Quick start 2: Monitoring applications with Azure Monitor

You are tasked with managing 3 different Kubernetes clusters running different web apps in Azure. Things have been going fine for a while now but today morning your users started complaining about performance of applications in the **contosoretail2** kubernetes cluster. and the issue seems to keep deteriorating. You have to meet your 5 minutes SLA to find and fix the issue. You are in PANIC mode – with so many containers how you can figure out what’s wrong so quickly. Don’t worry, we will lead you there - but it’s going to be up to you to find the root causes.

1. Login to the [Microsoft Azure portal](https://portal.azure.com)
2. Click on the contosoretail2 kubernetes cluster that is pinned to the dashboard
![dashboard](images/Kubernetes-dashboard.png)
3. Scroll down the left navigation pane and click on **Health (preview)** to see the health of your cluster
![health](images/Health.png)
4. You can see the memory and CPU usage at a Node, Controller and Container level.
![health](images/Node.png)
5. **Task 1:** Click through the clusters, go through the CPU usage, container restarts etc. and see if you can find any issue
6. So, you have isolated the problem to a specific container. Now you need to find out the root causes. To take a look at the logs, click on the Containers tab at the top and click on **View Logs**
7. **Task 2** Find the process responsible for the performance issue

`#Use a query similar to this - INSERT QUERY`

8. Congratulations! You have found the root cause within the SLA and saved the day. While you are it, you have a bonus task.
9. **Bonus Task** Did you know that Log Analytics is a common store for metrics and log data for number of monitoring and management tools? As a customer, you can use Log Analytics not just for containers but other Azure services including Virtual Machines and Azure Security Center.  For this task, you need to run a query to find all the VM’s that are missing security patches. You can find examples here - https://docs.loganalytics.io/docs/Examples/Log-Analytics-Examples



