library ieee;
use ieee.std_logic_1164.all;
use ieee.std_logic_arith.all;
use ieee.std_logic_unsigned.all;
--发送模块
entity uartrx is
	port(clk:in std_logic;
		rst_n:in std_logic;
			rx:in std_logic;
		dataout:out std_logic_vector(7 downto 0);
		rdsig:out std_logic;
		frameerror:out std_logic);
end uartrx;

architecture behavioral of uartrx is
signal rxbuf,rxfall:std_logic;
signal idle:std_logic;
signal receive:std_logic;
signal cnt:integer range 255 downto 0;
constant stopbit:std_logic:= '1';
begin
--rx下降沿检测
process(clk)
begin
	if(rising_edge(clk))then
		rxbuf <= rx;
		rxfall <= rxbuf and (not rx);
	end if;
end process;
--串口接收启动程序
process(clk)
begin
	if((rxfall and (not idle)) = '1')then
		receive <= '1';
	elsif(cnt = 168)then
		receive <= '0';
	end if;
end process;
--串口接收程序
process(clk,rst_n)
begin
	if(rst_n = '0')then
		idle <= '0';
		cnt <= 0;
		rdsig <= '0';
		frameerror <= '0';
	elsif(rising_edge(clk))then
		if(receive = '1')then
			case cnt is
				when 0   => idle <= '1'; cnt <= cnt + 1; rdsig <= '0';
				when 24  => idle <= '1'; dataout(0) <= rx; cnt <= cnt + 1; rdsig <= '0';
				when 40  => idle <= '1'; dataout(1) <= rx; cnt <= cnt + 1; rdsig <= '0';
				when 56  => idle <= '1'; dataout(2) <= rx; cnt <= cnt + 1; rdsig <= '0';
				when 72  => idle <= '1'; dataout(3) <= rx; cnt <= cnt + 1; rdsig <= '0';
				when 88  => idle <= '1'; dataout(4) <= rx; cnt <= cnt + 1; rdsig <= '0';
				when 104 => idle <= '1'; dataout(5) <= rx; cnt <= cnt + 1; rdsig <= '0';
				when 120 => idle <= '1'; dataout(6) <= rx; cnt <= cnt + 1; rdsig <= '0';
				when 136 => idle <= '1'; dataout(7) <= rx; cnt <= cnt + 1; rdsig <= '0';
				when 152 => idle <= '1'; if(stopbit = rx)then frameerror <= '0';else frameerror <= '1';end if;						--停止位接收校验
												 cnt <= cnt + 1;rdsig <= '1';									--拉高rdsig告知外设可读
				when others => cnt <= cnt + 1;
			end case;
		else
			cnt <= 0;
			idle <= '0';
			rdsig <= '0';
		end if;
	end if;
end process;
end behavioral;