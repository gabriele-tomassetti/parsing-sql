grammar SQL;

statements          : (statement | ignore)+ EOF
                    ;

statement           : createStmt
                    ;

ignore              : .*? SEMICOLON
                    | COMMENT
                    ;

createStmt          : CREATE TABLE (IF NOT EXISTS)? tableName=name
                        LPAREN element (COMMA element)* RPAREN?
                        SEMICOLON
                    ;

element             : definition
                    | ignorable
                    ;

ignorable           : (PRIMARY? KEY | CONSTRAINT | SPECIAL_FEATURES | FULLTEXT) .*? (COMMA|RPAREN)                    

                    ;

definition          : name type defaultValue? nullability? attributes*;

type                : (INTEGER | INT) UNSIGNED?                                                     #integerType
                    | (TINYINT | SMALLINT) UNSIGNED?                                                #smallIntegerType
                    | TEXT                                                                          #textType
                    | BLOB (SUBTYPE type)?                                                          #blobType
                    | (VARCHAR|CHARVAR) LPAREN NUMBER RPAREN                                        #varcharType
                    | CHAR LPAREN NUMBER RPAREN                                                     #charType
                    | YEAR                                                                          #yearType
                    | DATETIME                                                                      #datetimeType
                    | TIMESTAMP TIMEZONE?                                                           #timestampType
                    | (NUMERIC | DECIMAL) (LPAREN precision=NUMBER (COMMA scale=NUMBER)? RPAREN)?   #decimalType
                    ;

nullability         : NOT? NULL;

defaultValue        : DEFAULT NULL                                              #defaultNull
                    | DEFAULT (TRUE | FALSE)                                    #defaultBool
                    | DEFAULT (CURRENT_TIMESTAMP | NOW LPAREN RPAREN)           #defaultTime                    
                    | DEFAULT QUOTE NAME QUOTE                                  #defaultString
                    | DEFAULT NUMBER                                            #defaultInt
                    | DEFAULT REAL                                              #defaultDouble
                    | DEFAULT NEXTVAL LPAREN SPECIAL_STRING RPAREN              #defaultNext  
                    | DEFAULT SPECIAL_STRING                                    #defaultSpecial
                    ;

attributes          : PRIMARY KEY
                    | AUTOINCREMENT
                    | CURRENT_TIMESTAMP
                    | ON         
                    | UPDATE           
                    ;

name                : QUOTE? NAME QUOTE? ;                    

CREATE              : C R E A T E;
TABLE               : T A B L E;
IF                  : I F;
NOT                 : N O T;
EXISTS              : E X I S T S;

INTEGER             : I N T E G E R;
INT                 : I N T;
SMALLINT            : S M A L L I N T;
TINYINT             : T I N Y I N T;
UNSIGNED            : U N S I G N E D;
NUMERIC             : N U M E R I C;
NULL                : N U L L;
TEXT                : T E X T;
VARCHAR             : V A R C H A R;
CHARVAR             : C H A R A C T E R ' ' V A R Y I N G;
CHAR                : C H A R;
DATETIME            : D A T E T I M E;
TIMESTAMP           : T I M E S T A M P;
BLOB                : B L O B;
SUBTYPE             : S U B UNDER T Y P E;
DECIMAL             : D E C I M A L;
YEAR                : Y E A R;

DEFAULT             : D E F A U L T;
CURRENT_TIMESTAMP   : C U R R E N T UNDER T I M E S T A M P;
TIMEZONE            : W I T H O U T ' ' T I M E ' ' Z O N E;
ON                  : O N;
UPDATE              : U P D A T E;
PRIMARY             : P R I M A R Y;
KEY                 : K E Y;
CONSTRAINT          : C O N S T R A I N T;
AUTOINCREMENT       : A U T O I N C R E M E N T
                    | A U T O UNDER I N C R E M E N T;
SPECIAL_FEATURES    : S P E C I A L UNDER F E A T U R E S;
FULLTEXT            : F U L L T E X T;

TRUE                : T R U E;
FALSE               : F A L S E;
NEXTVAL             : N E X T V A L;
NOW                 : N O W;

QUOTE               : [`']; 
LPAREN              : '(';
RPAREN              : ')';
COMMA               : ',';
SEMICOLON           : ';';
EQUAL               : '=';
DOUBLE_COLON        : '::';

COMMENT             : ('--' ~[\r\n]* | '/*' .*? ( '*/' | EOF ) ) -> skip;

SPACES              : [ \t\r\n] -> skip;
    
SPECIAL_STRING      : QUOTE? NAME QUOTE? DOUBLE_COLON NAME;
    
NUMBER              : DIGIT+;
REAL                : DIGIT+ '.' DIGIT+;
    
NAME                : [a-zA-Z0-9_]+
                    ;
    
ANY                 : . -> skip;
    
fragment UNDER      : '_';
    
fragment DIGIT      : [0-9];
    
fragment A          : [aA];
fragment B          : [bB];
fragment C          : [cC];
fragment D          : [dD];
fragment E          : [eE];
fragment F          : [fF];
fragment G          : [gG];
fragment H          : [hH];
fragment I          : [iI];
fragment J          : [jJ];
fragment K          : [kK];
fragment L          : [lL];
fragment M          : [mM];
fragment N          : [nN];
fragment O          : [oO];
fragment P          : [pP];
fragment Q          : [qQ];
fragment R          : [rR];
fragment S          : [sS];
fragment T          : [tT];
fragment U          : [uU];
fragment V          : [vV];
fragment W          : [wW];
fragment X          : [xX];
fragment Y          : [yY];
fragment Z          : [zZ];