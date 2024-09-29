CREATE TABLE IF NOT EXISTS Users (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    Name VARCHAR(255) NOT NULL,
    Email VARCHAR(255) NOT NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS Incomes (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    UserId UUID NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    Source VARCHAR(255) NOT NULL,
    Date TIMESTAMP NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Goals (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    UserId UUID NOT NULL,
    GoalAmount DECIMAL(18, 2) NOT NULL,
    Deadline TIMESTAMP NOT NULL,
    CurrentAmount DECIMAL(18, 2) NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Expenses (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    UserId UUID NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    Description VARCHAR(255) NOT NULL,
    Category VARCHAR(255) NOT NULL,
    Date TIMESTAMP NOT NULL,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Budgets (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    UserId UUID NOT NULL,
    Month TIMESTAMP NOT NULL,
    BudgetAmount DECIMAL(18, 2) NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS BudgetNotificationLog (
    UserId UUID NOT NULL,
    BudgetId UUID NOT NULL,
    Notified_80 BOOLEAN DEFAULT FALSE,
    Notified_90 BOOLEAN DEFAULT FALSE,
    Notified_100 BOOLEAN DEFAULT FALSE,
    PRIMARY KEY (UserId, BudgetId)
);

CREATE TABLE IF NOT EXISTS GoalNotificationLogs (
    Id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    UserId UUID NOT NULL,
    GoalId UUID NOT NULL,
    Notified50 BOOLEAN DEFAULT FALSE,
    Notified100 BOOLEAN DEFAULT FALSE,
    UNIQUE (UserId, GoalId)
);

CREATE OR REPLACE FUNCTION update_current_amount()
RETURNS TRIGGER AS $$
BEGIN
    UPDATE Goals
    SET CurrentAmount = (
        SELECT COALESCE(SUM(i.Amount), 0) - COALESCE(SUM(e.Amount), 0)
        FROM Incomes i
        LEFT JOIN Expenses e ON i.UserId = e.UserId AND DATE_TRUNC('month', e.Date) = DATE_TRUNC('month', CURRENT_DATE)
        WHERE i.UserId = NEW.UserId
    )
    WHERE UserId = NEW.UserId;

    PERFORM update_goal_notifications(NEW.UserId);

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION update_goal_notifications(user_id UUID)
RETURNS VOID AS $$
DECLARE
    goal RECORD;
BEGIN
    FOR goal IN
        SELECT * FROM Goals WHERE UserId = user_id
    LOOP
        IF goal.CurrentAmount < (goal.GoalAmount * 0.5) THEN
            UPDATE GoalNotificationLogs
            SET Notified50 = FALSE
            WHERE UserId = user_id AND GoalId = goal.Id;
        END IF;

        IF goal.CurrentAmount < goal.GoalAmount THEN
            UPDATE GoalNotificationLogs
            SET Notified100 = FALSE
            WHERE UserId = user_id AND GoalId = goal.Id;
        END IF;
    END LOOP;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trigger_update_current_amount_on_income
AFTER INSERT OR UPDATE ON Incomes
FOR EACH ROW
EXECUTE FUNCTION update_current_amount();

CREATE TRIGGER trigger_update_current_amount_on_expense
AFTER INSERT OR UPDATE ON Expenses
FOR EACH ROW
EXECUTE FUNCTION update_current_amount();
