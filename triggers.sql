-- Update nyu_ttmod_info table, add column for last_reward, a timestamp to track the last time rewards were issued
ALTER TABLE `<YOUR DB>`.`nyu_ttmod_info` ADD COLUMN last_reward TIMESTAMP DEFAULT CURRENT_TIMESTAMP;

-- Update character table, add column for received_reward, a timestamp to track when this player last received rewards
ALTER TABLE `<YOUR DB>`.`character` ADD COLUMN received_reward TIMESTAMP;

-- Update character table, add column for can_quit, a boolean flag to verify whether a player should be able to quit their guild
ALTER TABLE `<YOUR DB>`.`character` ADD COLUMN can_quit TINYINT(1) DEFAULT 0;

-- Trigger for setting guild on character creation
-- Table: character
-- Condition: before insert
BEGIN
    SET NEW.GuilID = 1;
END

-- Trigger for preventing player from quitting guild
-- Table: character
-- Condition: before update
BEGIN
    IF NEW.GuildID IS NULL AND OLD.can_quit = 0 AND NEW.can_quit = 0 THEN
        SET NEW.GuildID = OLD.GuildID
END

